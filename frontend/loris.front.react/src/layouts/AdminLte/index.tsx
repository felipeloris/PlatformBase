import React from 'react';
import { withRouter } from 'react-router-dom';
import { useSelector } from 'react-redux';
import classnames from 'classnames';

import useStyles from './styles';
import Header from './Header';
import Sidebar from './Sidebar';
import Footer from './Footer';
import { LayoutProvider, useLayoutState } from './context/LayoutContext';
import { IUserStateWrapper } from '../../store/user/user.d';
import Loading from '../../components/Loading';

interface IProps {
  history: any;
  location: any;
}

const Layout: React.FC<IProps> = props => {
  const isLoading = useSelector<IUserStateWrapper, boolean>(state => state.user.isLoading);
  const classes = useStyles();
  const layoutState = useLayoutState();

  return (
    <div className={classes.root}>
      <Loading isLoading={isLoading} />
      <LayoutProvider>
        <Header history={props.history} />
        <Sidebar />
        <div
          className={classnames(classes.content, {
            [classes.contentShift]: layoutState.isSidebarOpened,
          })}
        >
          <div className={classes.fakeToolbar} />
          <div className={classes.childrenContent}>
            {props.children}
            <div className={classes.push}></div>
          </div>
          <Footer />
        </div>
      </LayoutProvider>
    </div>
  );
};

export default withRouter(Layout);
