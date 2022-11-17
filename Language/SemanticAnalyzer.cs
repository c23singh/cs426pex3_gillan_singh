using CS426.node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;

namespace CS426.analysis
{
    class SemanticAnalyzer : DepthFirstAdapter
    {
        // Global Symbol Table
        // Example: Function definitions, data types, constants
        Dictionary<string, Definition> globalSymbolTable = new Dictionary<string, Definition>();

        // Local Symbol  (Gets constantly wiped out)
        // Examples: Local Variables
        Dictionary<string, Definition> localSymbolTable = new Dictionary<string, Definition>();

        // Decorated Parse Tree
        Dictionary<Node, Definition> decoratedParseTree = new Dictionary<Node, Definition>();

        string curFunc;


        public void PrintWarning(Token t, String message)
        {
            Console.WriteLine("Line " + t.Line + ", Col" + t.Pos + ": " + message);
        }

        public override void InAProgram(AProgram node)
        {
            // Create a Definition for Integers
            Definition intDefinition = new NumberDefinition();
            intDefinition.name = "int";

            // Create a Defnition for Strings
            Definition strDefinition = new StringDefinition();
            strDefinition.name = "string";

            // Register the definitions with the global symbol table
            globalSymbolTable.Add("int", intDefinition);
            globalSymbolTable.Add("string", strDefinition);
        }

        // --------------------------------------------------------------
        // Operands
        // --------------------------------------------------------------

        public override void OutAIntOperand(AIntOperand node)
        {
            // Create the definition
            Definition intDefinition = new NumberDefinition();
            intDefinition.name = "int";

            decoratedParseTree.Add(node, intDefinition);
        }

        public override void OutAFltOperand(AFltOperand node)
        {
            // Create the definition
            Definition floatDefinition = new NumberDefinition();
            floatDefinition.name = "float";

            decoratedParseTree.Add(node, floatDefinition);
        }

        public override void OutAStrOperand(AStrOperand node)
        {
            Definition strDefinition = new StringDefinition();
            strDefinition.name = "string";

            decoratedParseTree.Add(node, strDefinition);
        }

        public override void OutAVariableOperand(AVariableOperand node)
        {
            // Get Name of ID
            String varName = node.GetId().Text;

            Definition varDefinition;

            // Test to see if the varName is in the symbol table
            if (!localSymbolTable.TryGetValue(varName, out varDefinition))
            {
                PrintWarning(node.GetId(), varName + " does not exist!");
            }
            // Test to see if varDefinition is actually a variable
            else if (!(varDefinition is VariableDefinition))
            {
                PrintWarning(node.GetId(), varName + " is not a variable!");
            }
            // Otherwise, we decorate the node with the variable type
            else
            {
                VariableDefinition v = (VariableDefinition)varDefinition;
                decoratedParseTree.Add(node, v.variableType);
            }
        }

        // --------------------------------------------------------------
        // Expression 6
        // --------------------------------------------------------------

        public override void OutAPassExpression6(APassExpression6 node)
        {
            Definition operandDefinition;

            if (!decoratedParseTree.TryGetValue(node.GetOperand(), out operandDefinition))
            {
                // Error would have bee caught at a lower level so no need to print
            }
            else
            {
                decoratedParseTree.Add(node, operandDefinition);
            }
        }

        public override void OutANegativeExpression6(ANegativeExpression6 node)
        {
            Definition operandDefinition;

            if (!decoratedParseTree.TryGetValue(node.GetOperand(), out operandDefinition))
            {
                // Error would have bee caught at a lower level so no need to print
            }
            else if (!(operandDefinition is NumberDefinition))
            {
                PrintWarning(node.GetMinus(), "Only a number can be negated!");
            }
            else
            {
                decoratedParseTree.Add(node, operandDefinition);
            }
        }

        

        // --------------------------------------------------------------
        // Expression 5
        // --------------------------------------------------------------

        public override void OutAPassExpression5(APassExpression5 node)
        {
            Definition operandDefinition;

            if (!decoratedParseTree.TryGetValue(node.GetExpression6(), out operandDefinition))
            {
                // Error would have bee caught at a lower level so no need to print
            }
            else
            {
                decoratedParseTree.Add(node, operandDefinition);
            }
        }

        public override void OutAParenthesisExpression5(AParenthesisExpression5 node)
        {
            Definition expressionDef;

            if (!decoratedParseTree.TryGetValue(node.GetExpression(), out expressionDef))
            {
                // There was an error, but we don't have to print it here
            }
            else if (!(expressionDef is NumberDefinition))
            {
                PrintWarning(node.GetOpenp(), "Only number types can be in parenthesis");
            }
            else
            {
                decoratedParseTree.Add(node, expressionDef);
            }
        }

        // --------------------------------------------------------------
        // Expression 4
        // --------------------------------------------------------------

        public override void OutAPassExpression4(APassExpression4 node)
        {
            Definition operandDefinition;

            if (!decoratedParseTree.TryGetValue(node.GetExpression5(), out operandDefinition))
            {
                // Error would have bee caught at a lower level so no need to print
            }
            else
            {
                decoratedParseTree.Add(node, operandDefinition);
            }
        }

        public override void OutADivideExpression4(ADivideExpression4 node)
        {
            Definition expression4Def;
            Definition expression5Def;

            if (!decoratedParseTree.TryGetValue(node.GetExpression4(), out expression4Def))
            {
                // There was an error, but we don't have to print it here
            }
            else if (!decoratedParseTree.TryGetValue(node.GetExpression5(), out expression5Def))
            {
                // There was an error, but we don't have to print it here
            }
            else if (expression4Def.GetType() != expression5Def.GetType())
            {
                PrintWarning(node.GetDivide(), "Cannot divide " + expression4Def.name + " by " + expression5Def.name);
            }
            else if (!(expression4Def is NumberDefinition))
            {
                PrintWarning(node.GetDivide(), "Can only divide numbers");
            }
            else
            {
                decoratedParseTree.Add(node, expression4Def);
            }
        }

        // --------------------------------------------------------------
        // Expression 3
        // --------------------------------------------------------------

        public override void OutAPassExpression3(APassExpression3 node)
        {
            Definition operandDefinition;

            if (!decoratedParseTree.TryGetValue(node.GetExpression4(), out operandDefinition))
            {
                // Error would have bee caught at a lower level so no need to print
            }
            else
            {
                decoratedParseTree.Add(node, operandDefinition);
            }
        }

        public override void OutAMultiplyExpression3(AMultiplyExpression3 node)
        {
            Definition expression3Def;
            Definition expression4Def;

            if(!decoratedParseTree.TryGetValue(node.GetExpression3(), out expression3Def))
            {
                // There was an error, but we don't have to print it here
            }
            else if (!decoratedParseTree.TryGetValue(node.GetExpression4(), out expression4Def))
            {
                // There was an error, but we don't have to print it here
            }
            else if (expression3Def.GetType() != expression4Def.GetType())
            {
                PrintWarning(node.GetMult(), "Cannot multiply " + expression3Def.name + " by " + expression4Def.name);
            }    
            else if (!(expression3Def is NumberDefinition))
            {
                PrintWarning(node.GetMult(), "Can only multiply numbers");
            }
            else
            {
                decoratedParseTree.Add(node, expression3Def);
            }
        }

        // --------------------------------------------------------------
        // Expression 2
        // --------------------------------------------------------------

        public override void OutAPassExpression2(APassExpression2 node)
        {
            Definition operandDefinition;

            if (!decoratedParseTree.TryGetValue(node.GetExpression3(), out operandDefinition))
            {
                // Error would have bee caught at a lower level so no need to print
            }
            else
            {
                decoratedParseTree.Add(node, operandDefinition);
            }
        }

        public override void OutASubtractExpression2(ASubtractExpression2 node)
        {
            Definition expression2Def;
            Definition expression3Def;

            if (!decoratedParseTree.TryGetValue(node.GetExpression2(), out expression2Def))
            {
                // There was an error, but we don't have to print it here
            }
            else if (!decoratedParseTree.TryGetValue(node.GetExpression3(), out expression3Def))
            {
                // There was an error, but we don't have to print it here
            }
            else if (expression2Def.GetType() != expression3Def.GetType())
            {
                PrintWarning(node.GetMinus(), "Cannot subtract " + expression2Def.name + " by " + expression3Def.name);
            }
            else if (!(expression2Def is NumberDefinition))
            {
                PrintWarning(node.GetMinus(), "Can only subtract numbers");
            }
            else
            {
                decoratedParseTree.Add(node, expression2Def);
            }
        }

        // --------------------------------------------------------------
        // Expression 
        // --------------------------------------------------------------

        public override void OutAPassExpression(APassExpression node)
        {
            Definition operandDefinition;

            if (!decoratedParseTree.TryGetValue(node.GetExpression2(), out operandDefinition))
            {
                // Error would have bee caught at a lower level so no need to print
            }
            else
            {
                decoratedParseTree.Add(node, operandDefinition);
            }
        }

        public override void OutAAddExpression(AAddExpression node)
        {
            Definition expressionDef;
            Definition expression2Def;

            if (!decoratedParseTree.TryGetValue(node.GetExpression(), out expressionDef))
            {
                // There was an error, but we don't have to print it here
            }
            else if (!decoratedParseTree.TryGetValue(node.GetExpression2(), out expression2Def))
            {
                // There was an error, but we don't have to print it here
            }
            else if (expressionDef.GetType() != expression2Def.GetType())
            {
                PrintWarning(node.GetPlus(), "Cannot add " + expressionDef.name + " by " + expression2Def.name);
            }
            else if (!(expressionDef is NumberDefinition))
            {
                PrintWarning(node.GetPlus(), "Can only add numbers");
            }
            else
            {
                decoratedParseTree.Add(node, expressionDef);
            }
        }

        // --------------------------------------------------------------
        // Declare Statement 
        // --------------------------------------------------------------

        public override void OutADeclareStatement(ADeclareStatement node)
        {
            Definition typeDef;
            Definition idDef;

            if (!globalSymbolTable.TryGetValue(node.GetType().Text, out typeDef))
            {
                PrintWarning(node.GetType(), "Type " + node.GetType().Text + " does not exist!");
            }
            else if (!(typeDef is TypeDefinition))
            {
                PrintWarning(node.GetType(), "Identifier " + node.GetType().Text + " is not a recognized data type");
            }
            else if (localSymbolTable.TryGetValue(node.GetVarname().Text, out idDef))
            {
                PrintWarning(node.GetVarname(), "Identifier " + node.GetVarname().Text + " is already being used");
            }
            else
            {
                VariableDefinition newVariableDefinition = new VariableDefinition();
                newVariableDefinition.name = node.GetVarname().Text;
                newVariableDefinition.variableType = (TypeDefinition)typeDef;

                localSymbolTable.Add(node.GetVarname().Text, newVariableDefinition);
            }
        }

        // --------------------------------------------------------------
        // Assignment
        // --------------------------------------------------------------

        public override void OutAAssignStatement(AAssignStatement node)
        {
            Definition idDef;
            Definition expressionDef;

            if (!localSymbolTable.TryGetValue(node.GetId().Text, out idDef))
            {
                PrintWarning(node.GetId(), "Identifier " + node.GetId().Text + " does not exist");
            }
            else if (!(idDef is VariableDefinition))
            {
                PrintWarning(node.GetId(), "Identifier " + node.GetId().Text + " is not a variable");
            }
            else if(!decoratedParseTree.TryGetValue(node.GetExpression(), out expressionDef))
            {
                // No need to print error message 
            }
            else if (((VariableDefinition)idDef).variableType.name != expressionDef.name)
            {
                PrintWarning(node.GetId(), "Types don't match");
            }
            else
            {
                // No need to do anything here
            }


        }

        // --------------------------------------------------------------
        // Comparison (types must match and must be a number)
        // --------------------------------------------------------------

        public override void OutANocompareComparison(ANocompareComparison node)
        {
            Definition boolDefinition = new BooleanDefinition();
            boolDefinition.name = "bool";
            Definition expressionDef;

            if(!decoratedParseTree.TryGetValue(node.GetExpression(), out expressionDef))
            {
                // There was an error, but we don't have to print it here
            }
            else if(!(expressionDef is NumberDefinition))
            {
                Console.WriteLine("Only numbers can be used in logical expressions!");
            }
            else
            {
                decoratedParseTree.Add(node, boolDefinition);
            }
        }

        public override void OutACompareComparison(ACompareComparison node)
        {
            Definition boolDefinition = new BooleanDefinition();
            boolDefinition.name = "bool";
            Definition leftExpressionDef;
            Definition rightExpressionDef;

            if (!decoratedParseTree.TryGetValue(node.GetLeft(), out leftExpressionDef))
            {
                // There was an error, but we don't have to print it here
            }
            else if (!decoratedParseTree.TryGetValue(node.GetRight(), out rightExpressionDef))
            {
                // There was an error, but we don't have to print it here
            }
            else if (!(leftExpressionDef is NumberDefinition))
            {
                Console.WriteLine("Only numbers can be used in logical expressions!");
            }
            else if (!(rightExpressionDef is NumberDefinition))
            {
                Console.WriteLine("Only numbers can be used in logical expressions!");
            }
            else
            {
                decoratedParseTree.Add(node, boolDefinition);
            }


        }

        // --------------------------------------------------------------
        // Logical 3
        // --------------------------------------------------------------

        public override void OutAPassLogical3(APassLogical3 node)
        {
            Definition comparisonDefinition;

            if (!decoratedParseTree.TryGetValue(node.GetComparison(), out comparisonDefinition))
            {
                // Error would have bee caught at a lower level so no need to print
            }
            else
            {
                decoratedParseTree.Add(node, comparisonDefinition);
            }
        }

        public override void OutANotLogical3(ANotLogical3 node)
        {
            Definition comparisonDefinition; 

            if (!decoratedParseTree.TryGetValue(node.GetComparison(), out comparisonDefinition))
            {
                // Error would have bee caught at a lower level so no need to print
            }
            else if (!(comparisonDefinition is BooleanDefinition))
            {
                PrintWarning(node.GetNotl(), "NOT can only be used on boolean values");
            }
            else
            {
                decoratedParseTree.Add(node, comparisonDefinition);
            }
        }

        // --------------------------------------------------------------
        // Logical 2
        // --------------------------------------------------------------

        public override void OutAPassLogical2(APassLogical2 node)
        {
            Definition logical3Definition;

            if (!decoratedParseTree.TryGetValue(node.GetLogical3(), out logical3Definition))
            {
                // Error would have bee caught at a lower level so no need to print
            }
            else
            {
                decoratedParseTree.Add(node, logical3Definition);
            }
        }

        public override void OutAOrLogical2(AOrLogical2 node)
        {
            Definition logical3Definition;
            Definition logical2Definition;

            if (!decoratedParseTree.TryGetValue(node.GetLogical3(), out logical3Definition))
            {
                // Error would have bee caught at a lower level so no need to print
            }
            else if (!decoratedParseTree.TryGetValue(node.GetLogical2(), out logical2Definition))
            {
                // Error would have bee caught at a lower level so no need to print
            }
            else if (!(logical2Definition is BooleanDefinition))
            {
                PrintWarning(node.GetOrl(), "Left hand side is not a boolean");
            }
            else if (!(logical3Definition is BooleanDefinition))
            {
                PrintWarning(node.GetOrl(), "Right hand side is not a boolean");
            }
            else
            {
                decoratedParseTree.Add(node, logical2Definition);
            }

        }

        // --------------------------------------------------------------
        // Logical 
        // --------------------------------------------------------------

        public override void OutAPassLogical(APassLogical node)
        {
            Definition logical2Definition;

            if (!decoratedParseTree.TryGetValue(node.GetLogical2(), out logical2Definition))
            {
                // Error would have bee caught at a lower level so no need to print
            }
            else
            {
                decoratedParseTree.Add(node, logical2Definition);
            }
        }

        public override void OutAAndLogical(AAndLogical node)
        {
            Definition logical2Definition;
            Definition logicalDefinition;

            if (!decoratedParseTree.TryGetValue(node.GetLogical2(), out logical2Definition))
            {
                // Error would have bee caught at a lower level so no need to print
            }
            else if (!decoratedParseTree.TryGetValue(node.GetLogical(), out logicalDefinition))
            {
                // Error would have bee caught at a lower level so no need to print
            }
            else if (!(logicalDefinition is BooleanDefinition))
            {
                PrintWarning(node.GetAndl(), "Left hand side is not a boolean");
            }
            else if (!(logical2Definition is BooleanDefinition))
            {
                PrintWarning(node.GetAndl(), "Right hand side is not a boolean");
            }
            else
            {
                decoratedParseTree.Add(node, logicalDefinition);
            }
        }

        // --------------------------------------------------------------
        // If/While
        // --------------------------------------------------------------

        public override void OutAIfStatement(AIfStatement node)
        {
            Definition logicalDef;

            if(!decoratedParseTree.TryGetValue(node.GetLogical(), out logicalDef))
            {
                // Error would have been caught at a lower level so no need to print
            }
            else if(!(logicalDef is BooleanDefinition))
            {
                PrintWarning(node.GetKeyif(), "Only booleans can be used with an if statement.");
            }
            else
            {
                // No need to do anything here
            }
        }

        public override void OutAWhileStatement(AWhileStatement node)
        {
            Definition logicalDef;

            if (!decoratedParseTree.TryGetValue(node.GetLogical(), out logicalDef))
            {
                // Error would have been caught at a lower level so no need to print
            }
            else if (!(logicalDef is BooleanDefinition))
            {
                PrintWarning(node.GetKeywhile(), "Only booleans can be used with a while statement.");
            }
            else
            {
                // No need to do anything here
            }
        }
        // --------------------------------------------------------------
        // Function Declare
        // --------------------------------------------------------------

        public override void InADefineFunction(ADefineFunction node)
        {
            Definition idDef;

            if(globalSymbolTable.TryGetValue(node.GetId().Text, out idDef))
            {
                PrintWarning(node.GetKeyfunc(), "Function named" + node.GetId().Text + " is already being used.");
            }
            else
            {
                FunctionDefinition newFunction = new FunctionDefinition();
                newFunction.name = node.GetId().Text;
                curFunc = newFunction.name;
                globalSymbolTable.Add(node.GetId().Text, newFunction);
            }
        }
        public override void OutADefineFunction(ADefineFunction node)
        {
            localSymbolTable.Clear();
        }

        public override void OutAMultipleFuncparams(AMultipleFuncparams node)
        {
            Definition typeDef;
            Definition idDef;
            Definition funcDef;


            if (!globalSymbolTable.TryGetValue(node.GetType().Text, out typeDef))
            {
                PrintWarning(node.GetType(), "Type " + node.GetType().Text + " does not exist!");
            }
            else if (!(typeDef is TypeDefinition))
            {
                PrintWarning(node.GetType(), "Identifier " + node.GetType().Text + " is not a recognized data type");
            }
            else if (!globalSymbolTable.TryGetValue(curFunc, out funcDef))
            {
                PrintWarning(node.GetComma(), "Function " + curFunc + " does not exi-st");
            }
            else
            {
                
            }
            
        }
    }

}
