# BillomatNet

This is an API client to access your data in [Billomat](https://www.billomat.com/). It is written as .NET Standard library to be used in .NET Framework projects and .NET Core projects as well. 

## Status

![Build status](https://travis-ci.org/martinhey/BillomatNet.svg?branch=master)
[![NuGet](https://img.shields.io/nuget/dt/TaurusSoftware.BillomatNet.svg)](https://www.nuget.org/packages/TaurusSoftware.BillomatNet/)

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
successively.

## Contribution

As already mentioned, the API is very powerful and it takes some time to implement all of it. My prio 1 is to implement reading access to clients and invoices. Feel free to contribute if you need more or want to support me.
