using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Cloak.Linq
{
	/// <summary>
	/// Evaluates and replaces independent sub-trees in expressions
	/// </summary>
	public static class ExpressionEvaluator
	{
		/// <summary>
		/// Evaluates and replaces independent sub-trees in the specified expression
		/// </summary>
		/// <param name="expression">The root of the expression tree</param>
		/// <param name="canBeEvaluated">A function which determines whether a node can be part of a subtree which can be evaluated</param>
		/// <returns>A new tree with independent subtrees evaluated and replaced</returns>
		[Pure]
		public static Expression EvaluateSubtrees(Expression expression, Func<Expression, bool> canBeEvaluated)
		{
			Contract.Requires(expression != null);
			Contract.Requires(canBeEvaluated != null);

			return new SubtreeEvaluator(canBeEvaluated).Evaluate(expression);
		}

		/// <summary>
		/// Evaluates and replaces independent sub-trees in the specified expression
		/// </summary>
		/// <param name="expression">The root of the expression tree</param>
		/// <returns>A new tree with independent subtrees evaluated and replaced</returns>
		[Pure]
		public static Expression EvaluateSubtrees(Expression expression)
		{
			Contract.Requires(expression != null);

			return new SubtreeEvaluator(CanBeEvaluatedLocally).Evaluate(expression);
		}

		private static bool CanBeEvaluatedLocally(Expression expression)
		{
			return expression.NodeType != ExpressionType.Parameter;
		}

		private sealed class SubtreeEvaluator : ExpressionVisitor
		{
			private readonly SubtreeNominator _nominator;
			private HashSet<Expression> _candidates;

			internal SubtreeEvaluator(Func<Expression, bool> canBeEvaluated)
			{
				_nominator = new SubtreeNominator(canBeEvaluated);
			}

			internal Expression Evaluate(Expression node)
			{
				_candidates = _nominator.NominateSubtrees(node);

				return Visit(node);
			}

			public override Expression Visit(Expression node)
			{
				if(node == null)
				{
					return node;
				}
				else if(_candidates.Contains(node))
				{
					return TryEvaluateCandidate(node);
				}
				else
				{
					return base.Visit(node);
				}
			}

			private static Expression TryEvaluateCandidate(Expression node)
			{
				return node.NodeType == ExpressionType.Constant ? node : EvaluateCandidate(node);
			}

			private static Expression EvaluateCandidate(Expression node)
			{
				var lambda = Expression.Lambda(node);

				var function = lambda.Compile();

				return Expression.Constant(function.DynamicInvoke(null), node.Type);
			}
		}

		private sealed class SubtreeNominator : ExpressionVisitor
		{
			private readonly Func<Expression, bool> _canBeEvaluated;
			private HashSet<Expression> _candidates;
			private bool _cannotBeEvaluated;

			internal SubtreeNominator(Func<Expression, bool> canBeEvaluated)
			{
				_canBeEvaluated = canBeEvaluated;
			}

			internal HashSet<Expression> NominateSubtrees(Expression node)
			{
				_candidates = new HashSet<Expression>();

				Visit(node);

				return _candidates;
			}

			public override Expression Visit(Expression node)
			{
				if(node != null)
				{
					TryNominateSubtree(node);
				}

				return node;
			}

			private void TryNominateSubtree(Expression node)
			{
				var priorCannotBeEvaluated = _cannotBeEvaluated;

				_cannotBeEvaluated = false;

				base.Visit(node);

				if(!_cannotBeEvaluated)
				{
					if(_canBeEvaluated(node))
					{
						_candidates.Add(node);
					}
					else
					{
						_cannotBeEvaluated = true;
					}
				}

				_cannotBeEvaluated |= priorCannotBeEvaluated;
			}
		}
	}
}