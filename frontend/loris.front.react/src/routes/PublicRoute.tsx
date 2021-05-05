import React from 'react';
import { Route, RouteProps } from 'react-router-dom';

import ContentRoute from './ContentRoute';

interface IProps extends RouteProps {
  component: React.ReactElement | any;
  layout?: React.ReactElement | any;
}

const PublicRoute: React.FC<IProps> = props => {
  const { component, layout, children, ...rest } = props;

  return (
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

export default PublicRoute;
