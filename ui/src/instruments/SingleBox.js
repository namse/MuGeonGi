import React, { Component } from 'react';
import styled from 'styled-components';
import Draggable from 'react-draggable';
import Jack from './Jack';
import save from '../utils/save';

const singleBoxMap = {}; // uuid, SingleBox
const singleBoxFindingJobs = {}; // uuid, handler

export function findSingleBox(uuid) {
  return new Promise((resolve, reject) => {
    const foundSingleBox = singleBoxMap[uuid];
    if (foundSingleBox) {
      resolve(foundSingleBox);
    } else {
      singleBoxFindingJobs[uuid] = singleBox => resolve(singleBox);
    }
  });
}

const singleBoxClickedHandlers = [];
export function onSingleBoxClicked(handler) {
  singleBoxClickedHandlers.push(handler);
}

const Container = styled.div`
  width: auto;
  height: auto;
  position: absolute;
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
  constructor(props) {
    super(props);
    this.state = {
      position: {
        x: 0,
        y: 0,
      },
    };
  }
  componentWillMount() {
    const { uuid } = this.props;
    singleBoxMap[uuid] = this;
    const handler = singleBoxFindingJobs[uuid];
    if (handler) {
      handler(this);
    }
  }
  componentWillUnmount() {
    const { uuid } = this.props;
    singleBoxMap[uuid] = undefined;
  }
  onDrag = (e, { x, y }) => {
    this.setState({ position: { x, y } });
  }
  onStop = (e, { x, y }) => {
    this.setState({ position: { x, y } });
    save();
  }
  setDefaultPosition(x, y) {
    this.setState({ position: { x, y } });
  }
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
      <Draggable
        handle=".content"
        ref={(ref) => { this.draggable = ref; }}
        position={this.state.position}
        onDrag={this.onDrag}
        onStop={this.onStop}
      >
        <Container
          onClick={() => singleBoxClickedHandlers.forEach(handler => handler(this))}
        >
          {this.renderJack(inputJack)}
          <Content className="content">
            a
            {this.props.children}
          </Content>
          {this.renderJack(outputJack)}
        </Container>
      </Draggable>
    );
  }
}
