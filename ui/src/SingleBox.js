import React, { Component } from 'react';
import styled from 'styled-components';
import Draggable from 'react-draggable';
import Jack from './Jack';

const Container = styled.div`
  width: auto;
  height: auto;
  display: inline-block;
  vertical-align: middle;
`;
const Content = styled.div`
  width: 100px;
  height: 100px;
  border: 1px solid black;
  position: relative;
  display: inline-block;
  vertical-align: middle;
`;
const JackContainer = styled.div`
  display: inline-block;
  width: 20px;
  height: 100px;
  position: relative;
  vertical-align: middle;
`;

export default class SingleBox extends Component {
  renderJack(jackProps) {
    if (!jackProps) {
      return false;
    }
    return (
      <JackContainer>
        <Jack {...jackProps} />
      </JackContainer>
    );
  }
  render() {
    const {
      inputJack,
      outputJack,
    } = this.props;
    return (
      <Draggable>
        <Container>
          {this.renderJack(inputJack)}
          <Content>
            a
            {this.props.children}
          </Content>
          {this.renderJack(outputJack)}
        </Container>
      </Draggable>
    );
  }
}
