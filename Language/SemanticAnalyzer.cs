using CS426.node;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if(!localSymbolTable.TryGetValue( varName, out varDefinition))
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
        // Declare Statement
        // --------------------------------------------------------------

        public override void OutADeclareStatement(ADeclareStatement node)
        {
            Definition typeDef;
            Definition idDef;

            if (!globalSymbolTable.TryGetValue(node.GetType().Text, out typeDef))
            {

            }
        }

    }
