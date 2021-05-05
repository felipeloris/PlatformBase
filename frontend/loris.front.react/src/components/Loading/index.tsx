import React from 'react';

import { Container } from './styled';

interface IProps {
  isLoading: boolean;
}

const Loading: React.FC<IProps> = props => {
  return props.isLoading ? (
    <Container>
      <div />
      <span>Carregando...</span>
    </Container>
  ) : (
    <></>
  );
};

export default Loading;
