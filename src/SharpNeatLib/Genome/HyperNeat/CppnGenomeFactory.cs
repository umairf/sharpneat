/* ***************************************************************************
 * This file is part of SharpNEAT - Evolution of Neural Networks.
 * 
 * Copyright 2004-2016 Colin Green (sharpneat@gmail.com)
 *
 * SharpNEAT is free software; you can redistribute it and/or modify
 * it under the terms of The MIT License (MIT).
 *
 * You should have received a copy of the MIT License
 * along with SharpNEAT; if not, see https://opensource.org/licenses/MIT.
 */

using SharpNeat.Genome.Neat;
using SharpNeat.Network;
using SharpNeat.Utils;

namespace SharpNeat.Genome.HyperNeat
{
    /// <summary>
    /// A sub-class of NeatGenomeFactory for creating CPPN genomes.
    /// </summary>
    public class CppnGenomeFactory : NeatGenomeFactory
    {
        #region Constructors

        /// <summary>
        /// Constructs with default NeatGenomeParameters, ID generators initialized to zero and a
        /// default IActivationFunctionLibrary.
        /// </summary>
        public CppnGenomeFactory(int inputNeuronCount, int outputNeuronCount)
            : base(inputNeuronCount, outputNeuronCount, DefaultActivationFunctionLibrary.CreateLibraryCppn())
        {
        }

        /// <summary>
        /// Constructs with default NeatGenomeParameters, ID generators initialized to zero and the
        /// provided IActivationFunctionLibrary.
        /// </summary>
        public CppnGenomeFactory(int inputNeuronCount, int outputNeuronCount,
                                 IActivationFunctionLibrary activationFnLibrary)
            : base(inputNeuronCount, outputNeuronCount, activationFnLibrary)
        {
        }

        /// <summary>
        /// Constructs with the provided IActivationFunctionLibrary and NeatGenomeParameters.
        /// </summary>
        public CppnGenomeFactory(int inputNeuronCount, int outputNeuronCount,
                                 IActivationFunctionLibrary activationFnLibrary,
                                 NeatGenomeParameters neatGenomeParams)
            : base(inputNeuronCount,outputNeuronCount, activationFnLibrary, neatGenomeParams)
        {
        }

        /// <summary>
        /// Constructs with the provided IActivationFunctionLibrary, NeatGenomeParameters and ID generators.
        /// </summary>
        public CppnGenomeFactory(int inputNeuronCount, int outputNeuronCount,
                                 IActivationFunctionLibrary activationFnLibrary,
                                 NeatGenomeParameters neatGenomeParams,
                                 Uint32Sequence genomeIdGenerator, Uint32Sequence innovationIdGenerator)
            : base(inputNeuronCount, outputNeuronCount, activationFnLibrary, neatGenomeParams, genomeIdGenerator, innovationIdGenerator)
        {
        }

        #endregion

        #region Public Methods [NeatGenome Specific / CPPN Overrides]

        /// <summary>
        /// Override that randomly assigns activation functions to neuron's from an activation function library
        /// based on each library item's selection probability.
        /// </summary>
        public override NeuronGene CreateNeuronGene(uint innovationId, NodeType neuronType)
        {
            int activationFnIdx;
            switch(neuronType)
            {
                case NodeType.Input:
                case NodeType.Output:
                {   // Use the ID of the first function. By convention this will be the Linear function but in actual 
                    // fact input neurons don't use their activation function.
                    activationFnIdx = 0;
                    break;
                }
                default:
                {
                    activationFnIdx = _activationFnLibrary.GetRandomFunctionIndex(_rng);
                    break;
                }
            }

            return new CppnNeuronGene(innovationId, neuronType, activationFnIdx);
        }

        #endregion
    }
}