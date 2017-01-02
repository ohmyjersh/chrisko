import React, { Component } from 'react';
import {bindActionCreators} from 'redux';
import { connect } from 'react-redux';
import * as actions from './chrisko';
import {Input, Button, Alert, Spin, Card } from 'antd';
import './App.css';

class App extends Component {
  componentWillMount() {
    if(this.props.params.key && !this.props.state.error)
    {
      this.props.actions.updateState({loadApp:false});
      this.props.actions.getChrisko(this.props.params.key);
    }
  }
  onClose = () => {
    this.props.actions.updateState({error:''});
  }
  createChrisko = () => {
    this.props.actions.updateState({error:''});
    this.props.actions.createChrisko(this.props.state.input);
  }
  onChange = (e)  => {
    this.props.actions.updateState({input:e.target.value});
  }

  onKeyDown = (e) => {
    if(e.key === 'Enter'){
      this.createChrisko();
    }
  }

  render() {
    console.log(this.props.state);
    return (
      <div className="App">
      { this.props.state.loadApp ? 
      <Card>
        <div style={styles.inputBar}>
          <Input style={styles.textInput} onChange={this.onChange}  onKeyDown={this.onKeyDown} addonAfter={<Button type="primary" onClick={this.createChrisko} >Submit</Button>} />
        </div>
        {this.props.state.fetching ? <Spin style={styles.spinner}/> : null}
          <ul>
            { 
               this.props.state.error ? 
               <li> 
                  <Alert message="Chrisko Error!"
                  description={this.props.state.error}
                  type="error"
                  closable
                  onClose={this.onClose}
                  showIcon/>
              </li> : null }
            {this.props.state.chriskos.map((x, index) => { 
              return <li key={index}>
                  <Alert message={`${x.id}`} description={`${x.url}`} type="success" showIcon />
                </li> 
            })}
          </ul>
        </Card> : null
      }
      </div>
    );
  }
}

const mapStateToProps = (state) => {
  return {
    state: state
  }
}

const mapDispatchToProps = (dispatch) => {
  return {
      actions: {
        getChrisko: bindActionCreators(actions.getChrisko,dispatch),
        createChrisko: bindActionCreators(actions.createChrisko, dispatch),
        updateState: bindActionCreators(actions.updateState, dispatch)
      }
    }
};

const AppContainer = connect(
  mapStateToProps,
  mapDispatchToProps
)(App)

export default AppContainer;


const styles = {
  card: {
    width: '',
    height: ''
  },
  spinner: {
    marginBottom: '10px',
    marginTop: '10px'
  },
  inputBar: {
    marginBottom: '10px'
  },
  textInput : {
    height: '50px',
    fontSize: '35px'
  }
}