import React from 'react';
import { CssBaseline } from '@material-ui/core';
import { BrowserRouter } from 'react-router-dom';

import GlobalStyles from './styles/GlobalStyles';
import CustomizedSnackbars from './components/CustomizedSnackbars';
import Routes from './routes';

const App: React.FC = () => {
  return (
    <BrowserRouter>
      <CssBaseline />
      <GlobalStyles />
      <CustomizedSnackbars />
      <Routes />
    </BrowserRouter>
  );
};

export default App;
