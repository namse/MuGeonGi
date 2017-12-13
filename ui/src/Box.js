import React, { Component } from 'react';
import styled from 'styled-components';

const Container = styled.div`
  width: 100px;
  height: auto;
  min-height: 100px;
  border: 1px solid black;
`;

export default class Box extends Component {
  render() {
    return (
      <Container>
        {this.props.children}
      </Container>
    );
  }
}
