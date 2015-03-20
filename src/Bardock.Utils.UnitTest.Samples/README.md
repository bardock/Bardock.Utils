# Unit Test Samples

This proyect demonstrate some good practices for unit testing. It covers:

* Readability: Naming conventions, setting up and tearing down, etc.
* xUnit basic usage
* Moq basic usage
* Dependency injection: register services globally and override per class and/or per test using autofac
* Mock data using Entity Framework and Effort: define shared data and per test
* Autofixture samples

## SUT

This folder contains sample classes used as the subject of some tests and their dependencies

## Mocks

This folder contains sample mocks and helpers to create Moq objects

## Setup

* ``AutofacDependencyResolverBootstrapper`` is used to register your project services globally
* TODO: Move ``AutofacDependencyResolverBootstrapperBase`` to another assembly

## Tests