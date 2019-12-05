# eShowOnWeb with Apache Ignite as data storage

This is a fork of https://github.com/dotnet-architecture/eShopOnWeb with data storage changed from Microsoft SQL Server to [Apache Ignite](https://ignite.apache.org/).

The aim is to demonstrate how Ignite can replace traditional SQL database in an enterprise application.

# Notes on implementation

TODO:

* int IDs - not good, replace with Long, use Ignite sequence
* Tests: easy to do integration tests with real in-process instance
* Thin and Thick APIs
* Identity implementation
* .Include()