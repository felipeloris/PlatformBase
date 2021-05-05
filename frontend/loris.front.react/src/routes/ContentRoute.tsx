import React from 'react';
import { RouteProps } from 'react-router-dom';

interface IProps {
  routeProps: RouteProps;
  component?: React.ReactElement | any;
  layout?: React.ReactElement | any;
  children?: React.ReactElement | any;
}

const ContentRoute: React.FC<IProps> = props => {
  const { component: Component, layout: Layout, children, routeProps } = props;

  return Layout ? (
    Component ? (
      <Layout>
        <Component {...routeProps} />
      </Layout>
    ) : (
      <Layout>children</Layout>
    )
  ) : Component ? (
    <Component {...routeProps} />
  ) : (
    children
  );
};

export default ContentRoute;
