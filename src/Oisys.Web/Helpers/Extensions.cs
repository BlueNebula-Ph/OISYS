namespace Oisys.Service.Helpers
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Custom extension methods
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Checks if an integer is null or zero
        /// </summary>
        /// <param name="number">The number to check</param>
        /// <returns>True if the number is null or zero, false otherwise</returns>
        public static bool IsNullOrZero(this int? number)
        {
            return !number.HasValue || number.Value == 0;
        }

        /// <summary>
        /// Retrieves the <see cref="DisplayAttribute.Name" /> property on the <see cref="DisplayAttribute" />
        /// of the current enum value, or the enum's member name if the <see cref="DisplayAttribute" /> is not present.
        /// </summary>
        /// <param name="val">This enum member to get the name for.</param>
        /// <returns>The <see cref="DisplayAttribute.Name" /> property on the <see cref="DisplayAttribute" /> attribute, if present.</returns>
        public static string GetDisplayName(this Enum val)
        {
            return val.GetType()
                      .GetMember(val.ToString())
                      .FirstOrDefault()
                      ?.GetCustomAttribute<DisplayAttribute>(false)
                      ?.Name
                      ?? val.ToString();
        }
    }
}
