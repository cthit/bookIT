using System;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace UnBookIT.Constraints;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class FormDeleteAttribute : Attribute, IActionConstraint
{
	public int Order => 0;

	public bool Accept(ActionConstraintContext context) =>
		context.RouteContext.HttpContext.Request.Form.TryGetValue("_method", out var method) &&
			method.Count == 1 && method[0].Equals("delete", StringComparison.OrdinalIgnoreCase);
}
