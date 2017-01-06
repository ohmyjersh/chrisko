import React, { Component } from 'react';
import {bindActionCreators} from 'redux';
import { connect } from 'react-redux';
import * as actions from './chrisko';
import {Input, Button, Alert, Spin, Card } from 'antd';
import Logo from './logo.png';

 export const InputBar = props => { 
  const createChrisko = (inputValue) => {
    console.log(inputValue);
      props.actions.updateState({error:''});
      props.actions.createChrisko(inputValue);
  }
  const onChange = (e)  => {
    props.actions.updateState({input:e.target.value});
  }

  const onKeyDown = (e, inputValue) => {
    if(e.key === 'Enter'){
      createChrisko(inputValue);
    }
  }
  return (
    <div style={styles.textInput}>
      <Input value={props.state.textInput} style={styles.textInput} onChange={onChange}  onKeyDown={(e) => onKeyDown(e, props.state.textInput)} addonAfter={<Button type="primary" onClick={(e) => createChrisko(props.state.textInput)}>Submit</Button>} />
    </div>
  )
}

  export const ErrorAlert = props => {
    const onClose = () => {
      props.actions.updateState({error:''});
    }
    return (
      <div>
        { 
          props.error ? 
               <li> 
                  <Alert message="Chrisko Error!"
                  description={props.error}
                  type="error"
                  closable
                  onClose={onClose}
                  showIcon/>
              </li> : null 
            } 
      </div>
    )
  }

  export const ChriskoAlerts = props => {
    console.log('alerts', props);
    return (
      <div>
        {props.chriskos.chriskos.map((x, index) => { 
          return <li key={index}>
              <Alert message={`${x.shortUrl}`} description={`${x.url}`} type="success" showIcon />
                </li> 
            })}
            </div>
    )
  }

  export const ChriskoList = props => {
    console.log('list', props);
    return (
        <ul>
            <ErrorAlert error={this.props.state.error} actions={this.props.actions}/>
            <ChriskoAlerts chriskos={this.props.state.chriskos}/>
        </ul>
    )
  }


class App extends Component {
  componentWillMount() {
    if(this.props.params.key && !this.props.state.error)
    {
      this.props.actions.updateState({loadApp:false});
      this.props.actions.getChrisko(this.props.params.key);
    }
  }

  render() {
    return (
      <div className="App" style={styles.app}>
      { this.props.state.loadApp ? 
      <Card>
        <img alt="logo" width="50%" src={Logo} />
        <InputBar state={{textInput: this.props.state.input}} actions={this.props.actions}/>
        {this.props.state.fetching ? <Spin style={styles.spinner}/> : null}
        <ChriskoList props={this.props.state}/>
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

export default connect(mapStateToProps,mapDispatchToProps)(App);


const styles = {
  app: {
    textAlign: 'center',
    marginRight: 'auto',
    marginLeft: 'auto',
    width: '800px',
    marginTop:'20px'
  },
  spinner: {
    marginBottom: '10px',
    marginTop: '10px'
  },
  inputBar: {
    marginTop:'10px',
    marginBottom: '10px'
  },
  textInput : {
    height: '50px',
    fontSize: '35px'
  }
}