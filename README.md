# eShowOnWeb with Apache Ignite as data storage

This is a fork of https://github.com/dotnet-architecture/eShopOnWeb with data storage changed from Microsoft SQL Server to [Apache Ignite](https://ignite.apache.org/).

The aim is to demonstrate how Ignite can replace traditional SQL database in an enterprise application.

# Notes on implementation

TODO:

* Guids instead of int ids - much better for a distributed system
* Not a relational DB - no constraints, but we can store entire object hierarchies for faster retrieval. "Include" calls are removed, but the code still works.
* Tests: easy to do integration tests with real in-process instance
* Thin and Thick APIs
* Identity implementation
* .Include()