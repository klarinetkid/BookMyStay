
-- provision the database, login & user
create database BookMyStay
use BookMyStay
create login BookMyStay with password = 'abcd1234', default_database = BookMyStay
create user BookMyStay from login BookMyStay
alter role db_owner add member BookMyStay