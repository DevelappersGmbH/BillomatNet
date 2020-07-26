# BillomatNet

This is an API client to access your data in [Billomat](https://www.billomat.com/). It is written as .NET Standard library to be used in .NET Framework projects and .NET Core projects as well. 

## Status

![Build and Test](https://github.com/DevelappersGmbH/BillomatNet/workflows/build%20and%20test/badge.svg)
[![Coverage Status](https://coveralls.io/repos/github/DevelappersGmbH/BillomatNet/badge.svg?branch=master)](https://coveralls.io/github/DevelappersGmbH/BillomatNet?branch=master)
[![Code Climate maintainability](https://img.shields.io/codeclimate/maintainability/DevelappersGmbH/BillomatNet)](https://codeclimate.com/github/DevelappersGmbH/BillomatNet)

![GitHub issues](https://img.shields.io/github/issues/DevelappersGmbH/BillomatNet)

![GitHub Release Date](https://img.shields.io/github/release-date/DevelappersGmbH/BillomatNet)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![NuGet](https://img.shields.io/nuget/dt/Develappers.BillomatNet.svg)](https://www.nuget.org/packages/Develappers.BillomatNet/)

## Usage

### Connect to your Billomat instance

First of all, you need an API Key for your user. To get one, please follow the [instructions](https://www.billomat.com/api/grundlagen/authentifizierung/).

BillomatNet is divided into several services, which correspond to the sections of the REST-API (e.g. InvoiceService for invoices, OfferService for offers and so on). Every service can be instanciated providing the credentials to authorize against your instance. Aas the API is stateless, you don't have to handle with sessions.
 
*Sample 1 (querying a filtered list of customers)*
```
var config = new Configuration
{
    ApiKey = "your_api_key",
    BillomatId = "your_billomat_id"
};

var service = new ClientService(config);

var query = new Query<Client, ClientFilter>()
    .AddFilter(x => x.Name, "GmbH")
    .AddSort(x => x.City, SortOrder.Ascending);

var clients = await service.GetListAsync(query, CancellationToken.None);
```

All lists are paged. The result contains information about the current page and the total number of items available. The query object built in the sample above also allows to specify the page (if omitted, you will get the first page containing 100 items according to the API spec of Billomat).

*Sample 2 (querying a customer)*
```
var config = new Configuration
{
    ApiKey = "your_api_key",
    BillomatId = "your_billomat_id"
};

var service = new ClientService(config);
var client = await service.GetById(435363);
```

## Project Status

The REST-API itself contains a whole bunch of functionality. This wrapper is still under development. New functions will be added 
successively. You can find detailed information about recent changes in [change log](CHANGELOG.md).

## Contribution

Bug reports and pull requests are welcome on [GitHub](https://github.com/DevelappersGmbH/BillomatNet). Please check the [contribution guide](CONTRIBUTING.md).This project is intended to be a safe, welcoming space for collaboration, and contributors are expected to our [code of conduct](CODE_OF_CONDUCT.md).
