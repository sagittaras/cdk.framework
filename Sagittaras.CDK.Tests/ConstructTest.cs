using Amazon.CDK;
using Amazon.CDK.Assertions;

namespace Sagittaras.CDK.Tests;

/// <summary>
/// Base class for easier writing of tests for constructs.
/// </summary>
public abstract class ConstructTest
{
    protected ConstructTest()
    {
        App = new App();
        Stack = new Stack(App);
    }
    
    /// <summary>
    /// Instance of App under which the stack is defined.
    /// </summary>
    protected App App { get; }
    
    /// <summary>
    /// Instance of stack to which the constructs within the test can be assigned.
    /// </summary>
    protected Stack Stack { get; } 
    
    /// <summary>
    /// Creates a new instance of Assertion Template from current stack state.
    /// </summary>
    protected Template StackTemplate => Template.FromStack(Stack);
}