# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.3.1]
### Added
- information about api request limits

### Changed
- created a new central entry point
- documentation

### Removed
- old constructors of service classes (BREAKING CHANGE !!!)

### Fixed
- nothing


## [0.2.1] - 2021-10-06
### Added
- RD operations on inbox documents
- R operation on purchase invoice documents
- R operations on suppliers

### Changed
- nothing

### Removed
- service collection extension to add all the services to IoC container (BREAKING CHANGE !!!)

### Fixed
- problems with default values during create


## [0.1.7] - 2021-01-18
### Fixed
- invoice items are now created by using default tax settings, when tax is not set
- due day, due date and tax settings aren't reset any more on edit of an invoice


## [0.1.6] - 2020-12-27
### Added
- implemented R operation on offers and offer items
- service collection extension to add all the services to IoC container
- implemented R operations for suppliers
- convinience method to determine portal url for entities

### Changed
- changed creation of invoices to use the template and pre configured values

### Fixed
- date filter problems in invoice service


## [0.1.5] - 2020-08-13
### Added
- implemented service for CUD operations on one contact
- implemented service for CUD operations on article
- implemented service for U operations on article property
- implemented service for CRD operation on article tag
- implemented service for R operations on client properties
- implemented service for CR operations on client property
- implemented service for R operations on client tags
- implemented service for CRD operations on one client tag
- implemented service for CUD operations on one contact
- implemented service for UD operations on one tax item
- implemented service for U operations on one invoice
- implemented service for C operations on invoice mail
- implemented service for CUD operations on one invoice item
- implemented service for CRD operations on one invoice comment
- implemented service for R operations on invoice comments
- implemented service for CRD operations on one invoice payment
- implemented service for R operations on invoice payments
- implemented service for R operations on one invoice payment
- implemented service for R operations on invoice payments
- implemented service for R operations on invoice one tag
- implemented service for CRD operations on invoice tags

### Changed
- GetList can throw an exception when not authorized now
- ToApiDate returns null when value is null

### Fixed
- converting SupplyDateType for CU operation on invoice
- Client Model and Mapper
- ToQueryString (ClientPropertyFilter) filters clients instead of articles


## [0.1.4] - 2020-07-16
### Added
- implemented service for CRUD operations on tax items
- implemented service for CRUD operations on units
- added querying settings when retrieving client information

### Changed
- write operations throw NotFoundException when the entity was not found
- validation check on create


## [0.1.3] - 2020-02-11
### Changed
- changed namespace (BREAKING CHANGE !!!)


## [0.1.2] - 2018-11-07
### Added
- retrieving tag cloud
- cancel and uncancel invoices
- completing invoices
- deleting invoices
- retrieving contacts

### Changed
- HTTP NotAuthorized will cause NotAuthorizedException
- GetById returns NULL when not found


## [0.1.1] - 2018-11-07
### Added
- retrieving invoices and invoice items
- retrieving a pdf for an invoice
