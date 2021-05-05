import { all } from 'redux-saga/effects';

import user from './user/sagas';

// eslint-disable-next-line @typescript-eslint/explicit-module-boundary-types
export default function* rootSaga() {
  return yield all([user]);
}
