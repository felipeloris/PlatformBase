import { ITreatedResultStatus } from './site.d';

export const TreatedResultStatus: ITreatedResultStatus = {
  Undefined: 0,
  Success: 1,
  Error: 2,
  NotValidate: 3,
  GoTo: 4,
  NoDataFound: 5,
  SuccessWarning: 6,
  CriticalError: 7,
  InternalServerError: 8,
  TimeoutError: 9,
  NotAuthorized: 10,
};

export type TTreatedResultStatus = typeof TreatedResultStatus[keyof typeof TreatedResultStatus];
