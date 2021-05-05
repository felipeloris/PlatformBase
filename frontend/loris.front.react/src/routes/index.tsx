import React from 'react';
import { Switch } from 'react-router-dom';

import PrivateRoute from './PrivateRoute';
import PublicRoute from './PublicRoute';
import Layout from '../layouts/AdminLte';
import SignIn from '../views/authentication/SignIn';
import ChangePassword from '../views/authentication/ChangePassword';
import ForgotPassword from '../views/authentication/ForgotPassword';
import Page404 from '../views/Page404';
import Dashboard from '../views/application/Dashboard';
import Users from '../views/administration/Users';
import Roles from '../views/administration/Roles';
import Resources from '../views/administration/Resources';
import User from '../views/administration/User';
import Role from '../views/administration/Role';
import Resource from '../views/administration/Resource';

const Routes: any = () => (
  <Switch>
    <PublicRoute exact path="/" component={SignIn} />
    <PublicRoute exact path="/signin" component={SignIn} />
    <PublicRoute exact path="/password_forgot" component={ForgotPassword} />
    <PublicRoute exact path="/password_change" component={ChangePassword} />
    <PrivateRoute path="/app" component={Dashboard} layout={Layout} loginView={SignIn} />
    <PrivateRoute exact path="/admin" component={Users} layout={Layout} loginView={SignIn} />
    <PrivateRoute path="/admin/users" component={Users} layout={Layout} loginView={SignIn} />
    <PrivateRoute path="/admin/roles" component={Roles} layout={Layout} loginView={SignIn} />
    <PrivateRoute
      path="/admin/resources"
      component={Resources}
      layout={Layout}
      loginView={SignIn}
    />
    <PrivateRoute path="/admin/user" component={User} layout={Layout} loginView={SignIn} />
    <PrivateRoute path="/admin/role" component={Role} layout={Layout} loginView={SignIn} />
    <PrivateRoute path="/admin/resource" component={Resource} layout={Layout} loginView={SignIn} />
    <PublicRoute path="*" component={Page404} />
  </Switch>
);

export default Routes;
