import React, { Component } from 'react';
import {bindActionCreators} from 'redux';
import { connect } from 'react-redux';
import * as actions from './chrisko';
import {Input, Button, Alert, Spin, Card } from 'antd';
import Logo from './logo.png';


export const inputBar = (barStyles, onChange,onKeyDown, createChrisko, textInput) => { 
  return (
    <div style={barStyles.textInput}>
      <Input value={textInput} style={barStyles.textInput} onChange={onChange}  onKeyDown={(e) => onKeyDown(e, textInput)} addonAfter={<Button type="primary" onClick={(e) => createChrisko(textInput)}>Submit</Button>} />
    </div>
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
  onClose = () => {
    this.props.actions.updateState({error:''});
  }
  createChrisko = (inputValue) => {
    console.log(inputValue);
      this.props.actions.updateState({error:''});
      this.props.actions.createChrisko(inputValue);
  }
  onChange = (e)  => {
    this.props.actions.updateState({input:e.target.value});
  }

  onKeyDown = (e, inputValue) => {
    if(e.key === 'Enter'){
      this.createChrisko(inputValue);
    }
  }

  render() {
    console.log(this.props.state.input);
    return (
      <div className="App" style={styles.app}>
      { this.props.state.loadApp ? 
      <Card>
        <img alt="logo" width="50%" src={Logo} />
        {inputBar(styles,this.onChange,this.onKeyDown, this.createChrisko, this.props.state.input)}
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
                  <Alert message={`${x.shortUrl}`} description={`${x.url}`} type="success" showIcon />
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