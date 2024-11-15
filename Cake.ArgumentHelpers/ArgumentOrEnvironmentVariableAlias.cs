using System;
using Cake.Common;
using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.ArgumentHelpers
{
    /// <summary>
    /// Contains Aliases for helping work with combinations of Argument and Environment variables.
    /// </summary>
    [CakeAliasCategory("Arguments")]
    [CakeAliasCategory("Environment")]
    public static class ArgumentOrEnvironmentVariableAlias
    {
        /// <summary>
        /// Get a bool variable from various script inputs: first via Argument, then falling back on EnvironmentVariable, finally falling back on a default.
        /// </summary>
        /// <param name="context">Cake context.</param>
        /// <param name="name">The argument name to attempt to find in the command line parameters, prefixing with <paramref name="environmentNamePrefix"/> to attempt to find in environment variables.</param>
        /// <param name="environmentNamePrefix">An optional prefix used to qualify the same variable name when present in EnvironmentVariable form (e.g., "MySetting" command-line argument vs. "MyTool_MySetting" environment variable).</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Value found or default, first checked in command-line argument, then environment variable.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory("Argument or environment variable")]
        public static bool ArgumentOrEnvironmentVariable(this ICakeContext context, string name, string environmentNamePrefix, bool defaultValue) =>
            ArgumentAliases.Argument(context, name, EnvironmentAliases.EnvironmentVariable(context, (environmentNamePrefix ?? string.Empty) + name) ?? defaultValue.ToString()).Equals("true", StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Get a bool variable from various script inputs: first via Argument, then falling back on EnvironmentVariable, finally falling back on a default.
        /// </summary>
        /// <param name="context">Cake context.</param>
        /// <param name="name">The argument name to attempt to find in either the command line parameters or environment variables.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Value found or default, first checked in command-line argument, then environment variable.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory("Argument or environment variable")]
        public static bool ArgumentOrEnvironmentVariable(this ICakeContext context, string name, bool defaultValue)
        {
            return context.ArgumentOrEnvironmentVariable(name, null, defaultValue);
        }

        /// <summary>
        /// Get a string variable from various script inputs: first via Argument, then falling back on EnvironmentVariable, finally falling back on a default.
        /// </summary>
        /// <param name="context">Cake context.</param>
        /// <param name="name">The argument name to attempt to find in the command line parameters, prefixing with <paramref name="environmentNamePrefix"/> to attempt to find in environment variables.</param>
        /// <param name="environmentNamePrefix">An optional prefix used to qualify the same variable name when present in EnvironmentVariable form (e.g., "MySetting" command-line argument vs. "MyTool_MySetting" environment variable).</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Value found or default, first checked in command-line argument, then environment variable.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory("Argument or environment variable")]
        public static string ArgumentOrEnvironmentVariable(this ICakeContext context, string name, string environmentNamePrefix, string defaultValue)
        {
            return ArgumentAliases.Argument<string>(context, name, EnvironmentAliases.EnvironmentVariable(context, environmentNamePrefix + name)) ?? defaultValue;
        }

        /// <summary>
        /// Get a string variable from various script inputs: first via Argument, then falling back on EnvironmentVariable, finally falling back on a default.
        /// </summary>
        /// <param name="context">Cake context.</param>
        /// <param name="name">The argument name to attempt to find in the command line parameters, prefixing with <paramref name="environmentNamePrefix"/> to attempt to find in environment variables.</param>
        /// <param name="environmentNamePrefix">An optional prefix used to qualify the same variable name when present in EnvironmentVariable form (e.g., "MySetting" command-line argument vs. "MyTool_MySetting" environment variable).</param>
        /// <returns>Value found or default, first checked in command-line argument, then environment variable.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory("Argument or environment variable")]
        public static string ArgumentOrEnvironmentVariable(this ICakeContext context, string name, string environmentNamePrefix)
        {
            return ArgumentOrEnvironmentVariable(context, name, environmentNamePrefix, null);
        }
    }
}
