using System;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Common;

namespace Cake.ArgumentHelpers
{
    /// <summary>
    /// Contains Aliases for helping work with combinations of Argument and Environment variables.
    /// </summary>
	[CakeAliasCategory("Argument")]
    [CakeAliasCategory("Environment")]
    public static class ArgumentOrEnvironmentVariableAlias
    {
        /// <summary>
        /// Get a bool variable from various script inputs: first via Argument, then falling back on EnvironmentVariable, finally falling back on a default.
        /// </summary>
        /// <param name="environmentNamePrefix">An optional prefix used to qualify the same variable name when present in EnvironmentVariable form (e.g., "MySetting" command-line argument vs. "MyTool_MySetting" environment variable).</param>
        /// <returns>Value found or default, first checked in command-line argument, then environment variable.</returns>
        [CakeMethodAlias]
		[CakeAliasCategory("Argument")]
		[CakeAliasCategory("Environment")]
		public static bool ArgumentOrEnvironmentVariable(this ICakeContext context, string name, string environmentNamePrefix, bool defaultValue)
        {
            return ArgumentAliases.Argument(context, name, EnvironmentAliases.EnvironmentVariable(context, (environmentNamePrefix ?? "") + name) ?? defaultValue.ToString()).Equals("true", StringComparison.OrdinalIgnoreCase);
        }

		/// <summary>
		/// Get a bool variable from various script inputs: first via Argument, then falling back on EnvironmentVariable, finally falling back on a default.
		/// </summary>
		/// <returns>Value found or default, first checked in command-line argument, then environment variable.</returns>
		[CakeMethodAlias]
		[CakeAliasCategory("Argument")]
		[CakeAliasCategory("Environment")]
		public static bool ArgumentOrEnvironmentVariable(this ICakeContext context, string name, bool defaultValue)
		{
            return context.ArgumentOrEnvironmentVariable(name, null, defaultValue);
		}
    }
}
