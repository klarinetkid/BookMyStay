# BookMyStay: Reservation API Demo
This project is a simple, insecure web API designed to demonstrate clean code principles, showcasing an Entity Framework database-first approach with SQL Server.

Since the reservations are for entire days, for simplicity the start and end fields use the `date` type, rather than `datetime`. For example, a reservation for one day will have the same value for start and end. This makes it simpler to compare reservations and count how many days they span, and avoids any potential headaches with timezones on the client side.

Beyond the required fields, some basic rules apply when submitting reservations:
 * The end must be equal or greater to the start.
 * The reservation must not overlap another reservation.

The additional rules are as follows:
 * The reservation must start must be after today.
 * The reservation cannot last longer than 3 days.
 * The reservation cannot be made more than 30 days in advance.


## Reservation Object

The reservation object is defined as these fields below.

| Field     | Type       | Required | Notes |
|-----------|------------|----------|-------|
| id        | integer    |          | Primary key |
| firstName | string(30) | Yes      |            |
| lastName  | string(30) | Yes      |            |
| start     | date       | Yes      |            |
| end       | date       | Yes      |            |
| created   | datetime   |          | System generated |
| modified  | datetime   |          | System generated |

Here is an example reservation:

```json
{
    "id": 1,
    "firstName": "Zach",
    "lastName": "M",
    "start": "2025-01-12",
    "end": "2025-01-12",
    "created": "2025-01-11T15:26:40.57",
    "modified": "2025-01-11T16:46:09.367"
}
```


## API Endpoints

### `GET /reservation`
| Parameter | Type   | Required |
|-----------|--------|----------|
| id        |integer | Yes      |

Returns the reservation found with the id provided. Returns status code 404 if none is found.

### `POST /reservation`
| Parameter     | Type       | Required |
|-----------|------------|----------|
| firstName | string(30) | Yes      |
| lastName  | string(30) | Yes      |
| start     | date       | Yes      |
| end       | date       | Yes      |

Accepts multipart/form-data. [Validates](#validation-errors) the reservation and creates it in the database. The resulting reservation is returned in JSON format.

### `PUT /reservation`
| Parameter     | Type       | Required |
|-----------|------------|----------|
| id        |integer | Yes      |
| firstName | string(30) | Yes      |
| lastName  | string(30) | Yes      |
| start     | date       | Yes      |
| end       | date       | Yes      |

Accepts multipart/form-data. [Validates](#validation-errors) the reservation and replaces the existing reservation with the same `id` with the one provided. The resulting reservation is returned in JSON format. Returns status code 404 if the reservation is not found.

### `DELETE /reservation`
| Parameter | Type   | Required |
|-----------|--------|----------|
| id        |integer | Yes      |

Deletes the reservation found with the id provided. Returns status code 404 if none is found.

### `GET /reservation/all`
Returns a list of all reservations in JSON format.

### `GET /reservation/past`
Returns a list of all reservations where the end date is before today in JSON format.

### `GET /reservation/current`
Returns a list of all reservations where the end date is equal to or greater than today in JSON format.


## Validation Errors
When a reservation is submitted, it must pass model validation before being written to the database. If any model errors are present, status code 400 is returned with a list of validation errors in JSON format.
```json
{
  "validationErrors": [
    "Reservation end cannot be before start."
  ]
}
```

## Database Setup
Use the SQL script `BookMyStay\BookMyStay.DataServices\Query\Provision.sql` to create the database and provision the SQL login and database user.

Run the SQL script `BookMyStay\BookMyStay.DataServices\Query\Create Tables.sql` to create the tables. Note, this script does not begin with a `use` statement to avoid executing in the wrong environment.

## Out of Scope Features
 * Authentication
 * Logging
 * List pagination
 * Unit tests
 * List query filters