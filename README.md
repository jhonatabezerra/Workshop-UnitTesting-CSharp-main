# Workshop - Unit Test / Automation Test with xUnit/nUnit/FluentAssertions - C# | .NET

How to create Unit Test or Automation Test with xUnit/nUnit/FluentAssertions and using C# / .NET

## Table of contents
* [Purpose](#purpose)
* [Challenge for test cases](#challenge-for-test-cases)
* [Instructions](#instructions)
* [What is unit test?](#what-is-unit-test)
* [How important is unit testing?](#how-important-is-unit-testing)
* [What is test coverage?](#what-is-test-coverage)
* [How to create a unit test?](#how-to-create-a-unit-test)
* [What is an integration test?](#what-is-an-integration-test)
* [How to create an integration test?](#how-to-create-an-integration-test)
* [Architecture](#architecture)

# Purpose
The purpose of this workshop is to show you how to create unit test scenarios and integration tests following best testing practices and using test libraries (xUnit, nUnit, FluentAssertions).
Some APIs have been created return values that are Mocking data to be used as test scenarios.

# Challenge for test cases
The purpose of this exercise is to implement a program that calculates the tax to be paid on profit or loss from operations in the financial stock market.

# Instructions

## How should the program work?

### Data input

Your program will receive a list of financial stock market operations in json via standard input ( stdin ). Each operation in this list contains the following fields:

| Name | Description |
| ------ | ------ |
| `operation` | Whether the operation is a buy (`buy`) or sell (`sell`) operation |
| `unit-cost`  | Share unit price |
| `quantity`  | Number of shares traded |

This is an example of the entry:
```json
[{"operation":"buy", "unit-cost":10, "quantity": 10000}, {"operation":"sell",
"unit-cost":20, "quantity": 5000}]
```
The last line of input will be an empty line.

### Data Output

The program should return a list containing the tax paid for each transaction received. You
elements of this list must be encoded in `json` format and the output must be returned
via standard output ( `stdout` ). The return consists of the following field:

| Name | Description |
| ------ | ------ |
| `tax` | The amount of tax paid on an operation |

This is an example of the output:

```json
[{"tax":0}, {"tax":10000}]
```

The list returned by the program must be the same size as the list of processed operations at the entrance. For example, if three operations were processed (`buy`, `buy`, `sell`), the return of the program must be a list with three values that represent the tax paid in each operation.

#### Capital Gain Rules

The program must handle two types of operations ( buy and sell ) and it must follow the following rules:
No tax is paid on purchase transactions;
To determine whether the operation resulted in a profit or a loss, you can calculate the **price weighted average**, where the weight corresponds to the number of shares purchased with certain price. For example, if you bought 10 shares for R$20 and 5 shares for R$10, the weighted average price is (10 x 20 + 5 x 10) / 15 = 16.66.
Losses happen when you sell shares at less than the average price purchase weight. In this case, no tax must be paid and you must subtract the loss of subsequent profits, before calculating the tax.
You do not pay any tax if the total value of the transaction (unit cost of the share x amount) is less than or equal to R$ 20000. Use the total amount of the operation and not the profit obtained to determine whether or not the tax should be paid. And don't forget to deduct the loss of subsequent profits.
The percentage of tax paid is 20% of the profit obtained. That is, the tax will be paid when there is a sales transaction whose price is higher than the weighted average price of purchase.
You must use past loss to deduct multiple future profits until all loss is deducted.

#### Examples of Capital Gain

##### Case #1

| Operation | Unit cost | Quantity | tax paid | Description |
| ------ | ------ | ------ | ------ | ------ |
| `buy` | 10 | 100 | 0 | Buying shares pays no tax |
| `sell` | 15 | 50 | 0 | Total amount less than $20000 |
| `sell` | 15 | 50 | 0 | Total amount less than $20000 |

###### Data Input

```json
[{"operation":"buy", "unit-cost":10, "quantity": 100},{"operation":"sell", "unitcost":15, "quantity": 50},{"operation":"sell", "unit-cost":15, "quantity": 50}]
```

###### Data Output

```json
[{"tax": 0},{"tax": 0},{"tax": 0}]
```

##### Case #2

| Operation | Unit cost | Quantity | tax paid | Description |
| ------ | ------ | ------ | ------ | ------ |
| `buy` | 10 | 10000 | 0 | Buying shares pays no tax |
| `sell` | 20 | 5000 | 10000 | Profit of $50000: 20% of the profit corresponds to $10000 and has no previous loss |
| `sell` | 5 | 5000 | 0 | Loss of $25000: does not pay tax |

###### Data Input

```json
[{"operation":"buy", "unit-cost":10, "quantity": 10000},{"operation":"sell",
"unit-cost":20, "quantity": 5000},{"operation":"sell", "unit-cost":5, "quantity":
5000}]
```

###### Data Output

```json
[{"tax": 0},{"tax": 10000},{"tax": 0}]
```

##### Case #3

| Operation | Unit cost | Quantity | tax paid | Description |
| ------ | ------ | ------ | ------ | ------ |
| `buy` | 10 | 10000 | 0 | Buying shares pays no tax |
| `sell` | 5 | 5000 | 0 | Loss of $25000: does not pay tax |
| `sell` | 20 | 5000 | 5000 | Profit of $50,000. Loss of $25,000 must be deducted: 20% of $25,000 -> $5000 |

###### Data Input

```json
[{"operation":"buy", "unit-cost":10, "quantity": 10000},{"operation":"sell",
"unit-cost":5, "quantity": 5000},{"operation":"sell", "unit-cost":20, "quantity":
5000}]
```

###### Data Output

```json
[{"tax": 0},{"tax": 0},{"tax": 5000}]
```

##### Case #4

| Operation | Unit cost | Quantity | tax paid | Description |
| ------ | ------ | ------ | ------ | ------ |
| `buy` | 10 | 10000 | 0 | Buying shares pays no tax |
| `buy` | 25 | 5000 | 0 | Buying shares pays no tax |
| `sell` | 15 | 10000 | 0 | Considering a weighted average price of $15 -> no profit or loss |

###### Data Input

```json
[{"operation":"buy", "unit-cost":10, "quantity": 10000},{"operation":"buy",
"unit-cost":25, "quantity": 5000},{"operation":"sell", "unit-cost":15,
"quantity": 10000}]
```

###### Data Output

```json
[{"tax": 0},{"tax": 0},{"tax": 0}]
```

##### Case #5

| Operation | Unit cost | Quantity | tax paid | Description |
| ------ | ------ | ------ | ------ | ------ |
| `buy` | 10 | 10000 | 0 | Buying shares pays no tax |
| `buy` | 25 | 5000 | 0 | Buying shares pays no tax |
| `sell` | 15 | 10000 | 0 | Considering a weighted average price of $15 -> no profit or loss |
| `sell` | 25 | 5000 | 10000 | Considering weighted average price of $15 -> profit of $50000; 20% of profit is $10000 |

###### Data Input

```json
[{"operation":"buy", "unit-cost":10, "quantity": 10000},{"operation":"buy",
"unit-cost":25, "quantity": 5000},{"operation":"sell", "unit-cost":15,
"quantity": 10000},{"operation":"sell", "unit-cost":25, "quantity": 5000}]
```

###### Data Output

```json
[{"tax": 0},{"tax": 0},{"tax": 0},{"tax": 10000}]
```

##### Case #6

| Operation | Unit cost | Quantity | tax paid | Description |
| ------ | ------ | ------ | ------ | ------ |
| `buy` | 10 | 10000 | 0 | Buying shares pays no tax |
| `sell` | 25 | 5000 | 0 | Loss of $40000: Total amount is less than $20000. But we must deduct the damage regardless. |
| `sell` | 15 | 10000 | 0 | $20,000 Profit: If you deduct the loss, your profit is $0 and you still have $20,000 of loss to deduct from your next profits. |
| `sell` | 25 | 5000 | 10000 | Profit of $20000: If you deduct the loss, your profit is $0 and you have no more loss to deduct from your next profits. |
| `sell` | 25 | 5000 | 10000 | $15000 profit and no loss: 20% profit tax. |

###### Data Input

```json
[{"operation":"buy", "unit-cost":10, "quantity": 10000},{"operation":"sell",
"unit-cost":2, "quantity": 5000},{"operation":"sell", "unit-cost":20, "quantity":
2000},{"operation":"sell", "unit-cost":20, "quantity": 2000},{"operation":"sell",
"unit-cost":25, "quantity": 1000}]
```

###### Data Output

```json
[{"tax": 0},{"tax": 0},{"tax": 0},{"tax": 0},{"tax": 3000}]
```

#### Application status
The program **must not depend** on any external database and the internal state of the application must be managed in memory explicitly by some structure it sees fit. The application state must be empty whenever the application is started.

#### Dealing with errors
You can assume that there will be no errors in converting the input json. In evaluating your solution we will not use entries that contain errors, are poorly formatted, or break the contract.

----

## What is unit test?
Unit testing is a way to test the functionality of only one resource to ensure that it is working the way it was designed to work.
If the functionality changes and the unit test continues to work, then this test has not been implemented correctly.

## How important is unit testing?
The importance of unit testing is to ensure that a feature will continue to work, even if new features are added. Unit tests are a guarantee for the developer/team that new code changes do not affect the functionality of others.

## What is test coverage?
Test coverage is a metric percentage of how much of our solution is covered by unit tests.
When creating a unit test, this test will call the method being tested. Each line of code that the interpreter passes is a line that will be covered. If a test is created to fail an IF and ELSE, if this test only passes the IF condition, the test will not cover the ELSE lines. Therefore, it is necessary to create two test cases to pass both conditions.

## How to create a unit test?
To create a unit test we need to create a test project.
By convention this project should be in a 'tests' folder.
The way to create the unit tests changes according to the library chosen for the test (Example: nUnit, xUnit).

## What is an integration test?
An integration of tests or integrated test is a type of test that validates our solution as a whole. His idea is not to specifically test features of our solution, but to test the entire functioning of the application as a whole.
This test normally tests if the application is correctly uploading with the instances and data needed to work.

----

# How to create an integration test?
BRD

----

# Architecture

```sh
-- src
  |__ ProjectTester.Domain
  |__ ProjectTester.Services
  |__ ProjectTester.WebApi
-- test
  |__ XUnitTests
     |__  ProjectTester.WebApi.Tests
```

## ProjectTester.Domain project
This project is responsible for containing the Domain (Models, Interfaces, Abstracts, Proprieties).

## ProjectTester.Services project
This project is responsible for containing all business logic or rules.

## ProjectTester.WebApi project
This project is responsible for containing the API controllers.

----

# What still needs to be implemented?

[ ] How to test an API;

[ ] How to test a library and its methods;

[ ] How to create a integration test;
