﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class OperationsCalculator : IOperations
    {
        private IList<string> operatorList = new List<string>();
        public IList<string> OperatorList
        {
            get { GetOperatorList(); return operatorList; }
            set { operatorList = value; }
        }
        public OperationsCalculator()
        {
            GetOperatorList();
        }
        private void GetOperatorList()
        {
            OperatorList = (BinaryOperations.Keys.ToList<string>().Concat(UnaryOperations.Keys.ToList<string>()).ToList<string>());
        }

        private Dictionary<string, int> priorityOperations = new Dictionary<string, int>
        {
            {"(", 0},
            {")", 0},
            {"+", 1},
            {"-", 1},
            {"*", 2},
            {"/", 2},
            {"_", 100} //унарный минус
        };

        public Dictionary<string, int> PriorityOperations
        {
            get { return priorityOperations; }
            set { priorityOperations = value; }
        }

        private Dictionary<string, Func<double, double, double>> binaryOperations = new Dictionary<string, Func<double, double, double>>
        {
            {"+", (a, b) => a + b},
            {"-", (a, b) => a - b},
            {"*", (a, b) => a * b},
            {"/", (a, b) => a / b}
        };

        public Dictionary<string, Func<double, double, double>> BinaryOperations
        {
            get { return binaryOperations; }
            set { binaryOperations = value; }
        }
        private Dictionary<string, Func<double, double>> unaryOperations = new Dictionary<string, Func<double, double>>
        {
            {"_", a => -a}
        };

        public Dictionary<string, Func<double, double>> UnaryOperations
        {
            get { return unaryOperations; }
            set { unaryOperations = value; }
        }
        public void AddBinaryOperation(string operation, Func<double, double, double> action)
        {

            if (BinaryOperations.ContainsKey(operation))
                throw new ArgumentException("Operation " + operation + "already exists.\n", "AddBinaryOperation");
            BinaryOperations.Add(operation, action);
        }
        public void AddUnaryOperation(string operation, Func<double, double> action)
        {

            if (BinaryOperations.ContainsKey(operation))
                throw new ArgumentException("Operation " + operation + "already exists.\n", "AddUnaryOperation");
            UnaryOperations.Add(operation, action);
        }
        public void SetPriorityOperations(string operation, int priority)
        {
            if (priority != 0)
                PriorityOperations.Add(operation, priority);
            else
                throw new ArgumentException("Priority must be greater than zero.\n", "SetPriorityOperations");
        }
    }
}
