﻿// Licensed to the Apache Software Foundation (ASF) under one
// or more contributor license agreements.  See the NOTICE file
// distributed with this work for additional information
// regarding copyright ownership.  The ASF licenses this file
// to you under the Apache License, Version 2.0 (the
// "License"); you may not use this file except in compliance
// with the License.  You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;

namespace Zfkr.SubscribeMysql.Protocol.Position
{
    [Serializable]
    public class PositionRange<T> where T : Position
    {
        public T Start { get; set; }

        /// <summary>
        /// add by ljh at 2012-09-05，用于记录一个可被ack的位置，保证每次提交到cursor中的位置是一个完整事务的结束
        /// </summary>
        public T Ack { get; set; }

        public T End { get; set; }

        public PositionRange()
        {
        }

        public PositionRange(T start, T end)
        {
            Start = start;
            End = end;
        }

        public override int GetHashCode()
        {
            const int prime = 31;
            var result = 1;
            result = prime * result + ((Ack == null) ? 0 : Ack.GetHashCode());
            result = prime * result + ((End == null) ? 0 : End.GetHashCode());
            result = prime * result + ((Start == null) ? 0 : Start.GetHashCode());
            return result;
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }

            if (obj == null)
            {
                return false;
            }

            if (!(obj is PositionRange<T>))
            {
                return false;
            }

            var other = (PositionRange<T>) obj;
            if (Ack == null)
            {
                if (other.Ack != null)
                {
                    return false;
                }
            }
            else if (!Ack.Equals(other.Ack))
            {
                return false;
            }

            if (End == null)
            {
                if (other.End != null)
                {
                    return false;
                }
            }
            else if (!End.Equals(other.End))
            {
                return false;
            }

            if (Start == null)
            {
                if (other.Start != null)
                {
                    return false;
                }
            }
            else if (!Start.Equals(other.Start))
            {
                return false;
            }

            return true;
        }
    }
}