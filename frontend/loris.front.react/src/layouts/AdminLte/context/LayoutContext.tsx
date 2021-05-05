/* eslint-disable @typescript-eslint/explicit-module-boundary-types */
import React from 'react';
import { Action } from 'redux';

interface ILayoutState {
  isSidebarOpened: boolean;
}

const initialState: ILayoutState = {
  isSidebarOpened: false,
};

const LayoutStateContext = React.createContext<ILayoutState>({} as ILayoutState);
const LayoutDispatchContext = React.createContext<React.Dispatch<ILayoutToggleSidebar>>(
  {} as React.Dispatch<ILayoutToggleSidebar>
);

//#region Type and Actions

export const LAYOUT_TOGGLE_SIDEBAR = 'LAYOUT_TOGGLE_SIDEBAR';
export interface ILayoutToggleSidebar extends Action {
  type: typeof LAYOUT_TOGGLE_SIDEBAR;
}

type TActions = ILayoutToggleSidebar;

const layoutToggleSidebar = (): ILayoutToggleSidebar => {
  return {
    type: LAYOUT_TOGGLE_SIDEBAR,
  };
};

//#endregion

function layoutReducer(state = initialState, action: TActions) {
  switch (action.type) {
    case LAYOUT_TOGGLE_SIDEBAR:
      return { ...state, isSidebarOpened: !state.isSidebarOpened };
    default: {
      throw new Error(`Unhandled action type: ${action.type}`);
    }
  }
}

const LayoutProvider: React.FC = ({ children }) => {
  const [state, dispatch] = React.useReducer(layoutReducer, initialState);

  return (
    <LayoutStateContext.Provider value={state}>
      <LayoutDispatchContext.Provider value={dispatch}>{children}</LayoutDispatchContext.Provider>
    </LayoutStateContext.Provider>
  );
};

const useLayoutState = () => {
  const context = React.useContext(LayoutStateContext);
  if (context === undefined) {
    throw new Error('useLayoutState must be used within a LayoutProvider');
  }
  return context;
};

const useLayoutDispatch = () => {
  const context = React.useContext(LayoutDispatchContext);
  if (context === undefined) {
    throw new Error('useLayoutDispatch must be used within a LayoutProvider');
  }
  return context;
};

export { LayoutProvider, useLayoutState, useLayoutDispatch, layoutToggleSidebar };
