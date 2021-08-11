# RPN
Reverse Polish Notation implementation

As I implemented this solution, I noted there are various ways of solving the RPN implementation.

At first I wanted to break the operands and the values into separate stacks. Although it worked for the main test cases, it would have required a lot of exception coding to handle the more "edge" cases. That proved to be inelegant and costly. In this final solution, I implemented a much simpler approach with more natural logic. This not only pays dividends in the initial development but will also lower the cost of maintenance coding.

I hate repeating code, so I strive for logic simplicity and reuse. You note the use a what I called the "mini-calculator". The parent method takes care of the parsing and traversing logic thus allowing the mini-calculator method to be small and focused and reusable.
In RunCalculator, the code for managing and manipulating the stack/array items is inelegant. That's where I believe the code can be refactored. RunCalculator is the largest method in the application so it would indicate that'd be a prime candidate for refactoring.

The stack collection proved to be unwieldly to manipulate so I went with lists and arrays. In a more elegant solution, I'd create a custom collection that allows for indexing ([]) and element removal natively.

I could have spent many more hours making this solution more elegant but I'm happy enough with the outcome and as Dennis said, "good enough" will suffice to get feedback for a version 2.

