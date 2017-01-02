import React from 'react'
import { render } from 'react-dom'
import { Router, Route, browserHistory } from 'react-router'
import { Provider } from 'react-redux'
import { createStore, applyMiddleware } from 'redux';
import thunk from 'redux-thunk';
import chrisko from './chrisko'
import App from './App'
import 'antd/dist/antd.css';  

const store = createStore(
  chrisko,
  applyMiddleware(thunk)
);

render(
  <Provider store={store}>
  <Router history={browserHistory}>
    <Route path="/(:key)" component={App} />
  </Router>
  </Provider>,
  document.getElementById('root')
)