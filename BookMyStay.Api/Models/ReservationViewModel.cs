using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BookMyStay.Api.Common;
using BookMyStay.DataServices.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookMyStay.Api.Models
{
    public class ReservationViewModel : BaseViewModel, IValidatableObject
    {
        #region Entity properties

        public int? Id { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "{0} length cannot exceed {1}.")]
        [DisplayName("First Name")]
        public string? FirstName { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "{0} length cannot exceed {1}.")]
        [DisplayName("Last Name")]
        public string? LastName { get; set; }

        [Required]
        public DateOnly? Start { get; set; }

        [Required]
        public DateOnly? End { get; set; }

        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }

        #endregion

        #region Helper properties/methods

        public int? DurationDays
        {
            get
            {
                if (!Start.HasValue || !End.HasValue) return null;

                // add one, since start & end same day is 1 day
                return End.Value.DayNumber - Start.Value.DayNumber + 1;
            }
        }

        public bool DoesReservationOverlap()
        {
            if (!Start.HasValue || !End.HasValue) return false;

            return dbContext.TblReservations.Any(e => !(e.End < Start || e.Start > End));
        }

        private TblReservation getTblReservation()
        {
            if (Id == null) throw new IdIsNullException();

            TblReservation? tblReservation = dbContext.TblReservations.Find(Id.Value);
            if (tblReservation == null) throw new ReservationNotFoundException();

            return tblReservation;
        }

        #endregion

        #region Conversion to/from DTO class

        private TblReservation toTblReservation()
        {
            return new TblReservation()
            {
                Id = Id.HasValue ? Id.Value : 0,
                FirstName = FirstName,
                LastName = LastName,
                Start = Start,
                End = End,
                Created = Created,
                Modified = Modified
            };
        }

        private void toReservationViewModel(TblReservation? tblReservation)
        {
            if (tblReservation != null)
            {
                Id = tblReservation.Id;
                FirstName = tblReservation.FirstName;
                LastName = tblReservation.LastName;
                Start = tblReservation.Start;
                End = tblReservation.End;
                Created = tblReservation.Created;
                Modified = tblReservation.Modified;
            }
        }

        #endregion

        #region Constructors

        public ReservationViewModel() { }

        public ReservationViewModel(TblReservation? tblReservation)
        {
            toReservationViewModel(tblReservation);
        }

        #endregion

        #region CRUD methods

        public void GetReservation()
        {
            TblReservation tblReservation = getTblReservation();
            toReservationViewModel(tblReservation);
        }

        public void CreateReservation()
        {
            if (Id != null) throw new IdIsNotNullException();

            TblReservation tblReservation = toTblReservation();
            tblReservation.Created = DateTime.Now;
            tblReservation.Modified = DateTime.Now;
            dbContext.TblReservations.Entry(tblReservation).State = EntityState.Added;
            dbContext.SaveChanges();
            toReservationViewModel(tblReservation); // to update Id value of this model
        }

        public void UpdateReservation()
        {
            // throws exception if id is null, reservation not found
            getTblReservation();

            TblReservation tblReservation = toTblReservation();
            tblReservation.Modified = DateTime.Now;
            dbContext.TblReservations.Entry(tblReservation).State = EntityState.Modified;

            // prevent created property from being updated
            dbContext.TblReservations.Entry(tblReservation).Property(e => e.Created).IsModified = false;

            dbContext.SaveChanges();
        }

        public void DeleteReservation()
        {
            TblReservation? tblReservation = getTblReservation();
            dbContext.TblReservations.Entry(tblReservation).State = EntityState.Deleted;
            dbContext.SaveChanges();
        }

        #endregion

        #region List methods

        public ReservationViewModel[] GetAllReservations()
        {
            return dbContext.TblReservations.Select(e => new ReservationViewModel(e)).ToArray();
        }

        public ReservationViewModel[] GetCurrentReservations()
        {
            return GetAllReservations().Where(e => e.End >= Helper.NowDateOnly).ToArray();
        }

        public ReservationViewModel[] GetPastReservations()
        {
            return GetAllReservations().Where(e => e.End < Helper.NowDateOnly).ToArray();
        }

        #endregion

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // validate start is not before today
            if (Start.HasValue && Start.Value <= Helper.NowDateOnly)
                yield return new ValidationResult(Strings.ReservationStartBeforeToday, [nameof(Start)]);

            // validate end is not greater than limit from today
            if (End.HasValue && End.Value > Helper.NowDateOnly.AddDays(Helper.AppConfig.MaxReservationDaysAhead))
                yield return new ValidationResult(Strings.ReservationTooFarAhead, [nameof(Start)]);

            if (Start.HasValue && End.HasValue)
            {
                // validate if end is before start
                if (End.Value < Start.Value)
                    yield return new ValidationResult(Strings.ReservationEndBeforeStart, [nameof(End)]);

                // validate duration does not exceed max stay
                if (DurationDays > Helper.AppConfig.MaxReservationDurationDays)
                    yield return new ValidationResult(Strings.ReservationDurationExceedsLimit, [nameof(End)]);
            }

            // check if any reservation overlaps
            if (DoesReservationOverlap())
                yield return new ValidationResult(Strings.ReservationOverlaps, [nameof(Start)]);
        }
    }
}
