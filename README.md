# NUnit_Demonstration
A collection of NUnit examples in C#.

I created this project as an exercise a few years ago when I wanted to learn more about how the [**NUnit**](https://nunit.org/) unit testing package worked.  I cleaned it up a bit later so I could use it to do **NUnit** webinars for my team, and I've updated it to include some more recent **NUnit** features.

The code is organized as collections of short tests, each demonstrating a variation on some feature of NUnit testing. Most of them have enough comments to explain what they are doing and what NUnit is expected to do. At the top level, they are arranged into the following broad categories:

- **Assertions** -- Examples of various types of testing using the original assertion style.
- **Constraints** -- Examples of many of the same tests using the constraint style.
- **Data Driven Tests** -- Examples of how to use test cases and other data sources.
- **Exception Testing** -- Examples of how to test for thrown exceptions.
- **Extensions** -- Examples of how to add your own constraints.
- **SetupAndTeardown** -- Examples of how lifecycle management works.
- **Utilities** -- Examples of useful methods and attributes, including theories.

**Note:** Some of these tests *will* fail, because they are demonstrating how **NUnit** handles certain failure cases. These are all named clearly, and they are all in the **ExpectedToFail** category.
