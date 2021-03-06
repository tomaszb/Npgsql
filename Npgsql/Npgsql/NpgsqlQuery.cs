// Npgsql.NpgsqlQuery.cs
//
// Author:
//     Dave Joyner <d4ljoyn@yahoo.com>
//
//    Copyright (C) 2002 The Npgsql Development Team
//    npgsql-general@gborg.postgresql.org
//    http://gborg.postgresql.org/project/npgsql/projdisplay.php
//
// Permission to use, copy, modify, and distribute this software and its
// documentation for any purpose, without fee, and without a written
// agreement is hereby granted, provided that the above copyright notice
// and this paragraph and the following two paragraphs appear in all copies.
//
// IN NO EVENT SHALL THE NPGSQL DEVELOPMENT TEAM BE LIABLE TO ANY PARTY
// FOR DIRECT, INDIRECT, SPECIAL, INCIDENTAL, OR CONSEQUENTIAL DAMAGES,
// INCLUDING LOST PROFITS, ARISING OUT OF THE USE OF THIS SOFTWARE AND ITS
// DOCUMENTATION, EVEN IF THE NPGSQL DEVELOPMENT TEAM HAS BEEN ADVISED OF
// THE POSSIBILITY OF SUCH DAMAGE.
//
// THE NPGSQL DEVELOPMENT TEAM SPECIFICALLY DISCLAIMS ANY WARRANTIES,
// INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY
// AND FITNESS FOR A PARTICULAR PURPOSE. THE SOFTWARE PROVIDED HEREUNDER IS
// ON AN "AS IS" BASIS, AND THE NPGSQL DEVELOPMENT TEAM HAS NO OBLIGATIONS
// TO PROVIDE MAINTENANCE, SUPPORT, UPDATES, ENHANCEMENTS, OR MODIFICATIONS.

using System;
using System.IO;
using System.Text;

namespace Npgsql
{
    /// <summary>
    /// Summary description for NpgsqlQuery
    /// </summary>
    internal abstract class NpgsqlQuery : ClientMessage
    {
        protected readonly byte[] commandText;

        public static NpgsqlQuery Create(ProtocolVersion protocolVersion, byte[] command)
        {
            switch (protocolVersion)
            {
                case ProtocolVersion.Version2 :
                    return new NpgsqlQueryV2(command);

                case ProtocolVersion.Version3 :
                    return new NpgsqlQueryV3(command);

                default :
                    throw new ArgumentException("Unknown protocol version");
            }
        }

        public static NpgsqlQuery Create(ProtocolVersion protocolVersion, string command)
        {
            switch (protocolVersion)
            {
                case ProtocolVersion.Version2 :
                    return new NpgsqlQueryV2(command);

                case ProtocolVersion.Version3 :
                    return new NpgsqlQueryV3(command);

                default :
                    throw new ArgumentException("Unknown protocol version");
            }
        }

        protected NpgsqlQuery(byte[] command)
        {
            commandText = command;
        }

        protected NpgsqlQuery(string command)
        {
            commandText = BackendEncoding.UTF8Encoding.GetBytes(command);
        }
    }
}
