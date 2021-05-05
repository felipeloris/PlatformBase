import React from 'react';

interface IfProps {
  children: React.ReactNode;
  test: boolean;
}

const If: React.FC<IfProps> = ({ children, test }): any => {
  if (test) {
    return children;
  } else {
    return null;
  }
};

export default If;
