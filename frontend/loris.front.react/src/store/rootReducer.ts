import { combineReducers } from 'redux';

import snackbar from './snackbar/reducer';
import user from './user/reducer';

const rootReducer = combineReducers({
  snackbar,
  user,
});

export type AppState = ReturnType<typeof rootReducer>;
export default rootReducer;
