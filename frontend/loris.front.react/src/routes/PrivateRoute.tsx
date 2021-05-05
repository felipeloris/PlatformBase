import React from 'react';
import { useSelector } from 'react-redux';
import { Route, Redirect, RouteProps } from 'react-router-dom';

import ContentRoute from './ContentRoute';
import { IUserStateWrapper } from '../store/user/user.d';

interface IProps extends RouteProps {
  component: React.ReactElement | any;
  loginView: React.ReactElement | any;
  layout?: React.ReactElement | any;
  children?: React.ReactElement | any;
}

const PrivateRoute: React.FC<IProps> = props => {
  const { component, layout, children, loginView, ...rest } = props;
  const isLogged = useSelector<IUserStateWrapper, boolean>(state => state.user.isLogged);

  return !isLogged ? (
    <Redirect to={loginView} />
  ) : (
    <Route
      {...rest}
      render={routeProps => (
        <ContentRoute
          component={component}
          layout={layout}
          routeProps={routeProps}
          // eslint-disable-next-line react/no-children-prop
          children={children}
        />
      )}
    />
  );
};

export default PrivateRoute;
