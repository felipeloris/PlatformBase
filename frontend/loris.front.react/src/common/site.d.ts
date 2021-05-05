export interface ISiteError {
  type: string;
  error: Error | string;
}

/*
export enum ETreatedResultStatus
{
    Undefined = 0,
    Success = 1,
    Error = 2,
    NotValidate = 3,
    GoTo = 4,
    NoDataFound = 5,
    SuccessWarning = 7,
    CriticalError = 8,
    InternalServerError = 9,
    TimeoutError = 10,
    NotAuthorized = 11
}
*/

export interface ITreatedResultStatus
{
  Undefined: number,
  Success: number,
  Error: number,
  NotValidate: number,
  GoTo: number,
  NoDataFound: number,
  SuccessWarning: number,
  CriticalError: number,
  InternalServerError: number,
  TimeoutError: number,
  NotAuthorized: number,
}

export interface ITreatedResult {
  result: any;
  status: number;
  message: string;
  error: boolean;
}

//#region Rest method 'GetByParemeter'

export interface ParameterFilter {
  field: string;
  condition: number;
  value: string;
}

export interface Parameter {
  sortField: string;
  sortOrder: number;
  pageIndex: number;
  pageSize: number;
  filters?: ParameterFilter[];
}

//endregion
