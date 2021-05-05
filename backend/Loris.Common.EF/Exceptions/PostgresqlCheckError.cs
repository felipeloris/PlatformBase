using Loris.Common.Domain.Interfaces;
using Npgsql;
using System;

namespace Loris.Common.Exceptions
{
    /// <summary>
    /// Postgresql Errors
    /// https://www.postgresql.org/docs/11/errcodes-appendix.html
    /// </summary>

    public class PostgresqlCheckError : DataCheckError
    {
        public string PostgresqlState { get; private set; }

        public override IDataCheckError GetNewCheckError(Exception ex)
        {
            IsDefined = false;
            ErrorMessage = ex?.Message;
            InternalException = ex;

            var postEx = (ex.InnerException as PostgresException);
            if (!string.IsNullOrEmpty(postEx?.SqlState))
            {
                InternalException = postEx;
                PostgresqlState = postEx.SqlState?.Trim();
                ErrorMessage = ex.InnerException.Message;

                #region Define the Postgresql error

                // Class 23 — Integrity Constraint Violation
                if (PostgresqlState.Equals("23000"))
                {
                    ErrorType = DataErrorTypeEnum.IntegrityConstraintViolation;
                }
                if (PostgresqlState.Equals("23001"))
                {
                    ErrorType = DataErrorTypeEnum.RestrictViolation;
                }
                if (PostgresqlState.Equals("23502"))
                {
                    ErrorType = DataErrorTypeEnum.NotNullViolation;
                }
                else if (PostgresqlState.Equals("23503"))
                {
                    ErrorType = DataErrorTypeEnum.ForeignKeyViolation;
                }
                else if (PostgresqlState.Equals("23505"))
                {
                    ErrorType = DataErrorTypeEnum.UniqueViolation;
                }
                else if (PostgresqlState.Equals("23514"))
                {
                    ErrorType = DataErrorTypeEnum.CheckViolation;
                }
                else if (PostgresqlState.Equals("23P01"))
                {
                    ErrorType = DataErrorTypeEnum.ExclusionViolation;
                }

                #endregion

                IsDefined = (ErrorType != DataErrorTypeEnum.Undefined);
            }

            return new DataCheckError(IsDefined, ErrorType, ErrorMessage, InternalException);
        }
    }
}
