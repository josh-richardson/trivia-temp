﻿using System;
using System.IO;
using NUnit.Framework;

namespace Trivia.Tests
{
    [TestFixture]
    public class GameRunnerShould
    {
        private TextWriter _output;
        StringReader input = new StringReader(Environment.NewLine);
        [SetUp]
        public void SetUp()
        {
            
            Console.SetIn(input);
            _output = new StringWriter();
            Console.SetOut(_output);
        }

        [Test]
        public void Run_until_there_is_a_winner()
        {
            GameRunner.Main(new[] {"test"});
            var expectedOutput = File.ReadAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "output.txt"));
            Assert.That(_output.ToString(), Is.EqualTo(expectedOutput));
            //File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "output.txt"), _output.ToString());
        }
    }
}
