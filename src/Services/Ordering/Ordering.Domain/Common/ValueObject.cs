using System;
using System.Collections.Generic;
using System.Linq;

namespace Ordering.Domain.Common
{
    public abstract class ValueObject
    {
        /// <summary>
        /// Check when two ValueObject's are Equal
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        protected static bool Equal(ValueObject left, ValueObject right)
        {
            //Return false if only one side is null
            if(left is null ^ right is null)
            {
                return false;
            }
            return left?.Equals(right) != false;
        }

        /// <summary>
        /// Check when two ValueObject's are Not equal
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        protected static bool NotEqual(ValueObject left, ValueObject right)
        {
            return !(Equal(left, right));
        }

        /// <summary>
        /// Get equal components within IEnmerable - abstract
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerable<object> GetEqualityComponents();

        /// <summary>
        /// Get all equal components to current object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            //If object is null or non existant
            if(obj == null || obj.GetType() != GetType())
            {
                return false;
            }
            var other = (ValueObject)obj; //Cast
            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        /// <summary>
        /// Get Hash Code for a given Equality Component
        /// XOR values within Components
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }
    }
}
