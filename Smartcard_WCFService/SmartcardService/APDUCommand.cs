﻿/**
 * @author Olivier ROUIT
 * 
 * @license CPL, CodeProject license 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GemCard.Service
{
    /// <summary>
    /// This class represents an APDU command to send to a smart card.
    /// 
    /// It is a DataContract to be serializd by a WCF service request
    /// </summary>
    [DataContract]
    public class APDUCommand
    {
        /// <summary>
        /// Get/set Class of the Coomqnd
        /// </summary>
        [DataMember]
        public byte Class
        {
            get;
            set;
        }

        /// <summary>
        /// Get/set the Instruction (Ins) of the command
        /// </summary>
        [DataMember]
        public byte Ins
        {
            get;
            set;
        }

        /// <summary>
        /// Get/set the Parameter P1 of the command
        /// </summary>
        [DataMember]
        public byte P1
        {
            get;
            set;
        }

        /// <summary>
        /// Get/set the Parameter P2 of the command
        /// </summary>
        [DataMember]
        public byte P2
        {
            get;
            set;
        }

        /// <summary>
        /// Get/set the Data of the command
        /// </summary>
        [DataMember]
        public byte[] Data
        {
            get;
            set;
        }

        /// <summary>
        /// Get/set the expected Length (Le) of the command
        /// </summary>
        [DataMember]
        public byte Le
        {
            get;
            set;
        }
    }
}
