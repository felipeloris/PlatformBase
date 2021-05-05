import storage from 'redux-persist/lib/storage';
import { persistReducer } from 'redux-persist';

// eslint-disable-next-line @typescript-eslint/explicit-module-boundary-types
export default (reducers: any) => {
  const persistedReducers = persistReducer(
    {
      key: '@loris.com.br',
      storage,
      whitelist: ['snackbar', 'user'],
    },
    reducers
  );

  return persistedReducers;
};
