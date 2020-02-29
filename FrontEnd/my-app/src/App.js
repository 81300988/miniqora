import React, {Component} from 'react';
import {BrowserRouter as Router, Route, Switch} from 'react-router-dom';
import './App.css';
import MuiThemeProvider from '@material-ui/core/styles/MuiThemeProvider';
import createMuiTheme from '@material-ui/core/styles/createMuiTheme';
// Redux
import {Provider} from 'react-redux';
import store from './redux/store';
import {SET_AUTHENTICATED} from './redux/types';
import {getUserData} from './redux/actions/userActions';
// Components
import Navbar from './components/layout/Navbar';
import themeObject from './util/theme';
import AuthRoute from './util/AuthRoute';
import RequiredLoginRoute from './util/RequiredLoginRoute';
// Pages
import home from './pages/home';
import login from './pages/login';
import signup from './pages/signup';
import category from './pages/category';
import question from './pages/question';

import axios from 'axios';

const theme = createMuiTheme(themeObject);
//Setup URL call to BackEnd here
axios.defaults.baseURL = process.env.REACT_APP_API_URL;

const token = localStorage.FBIdToken;
if (token) {
  store.dispatch({type: SET_AUTHENTICATED});
  axios.defaults.headers.common['Authorization'] = token;
  store.dispatch(getUserData());
}

class App extends Component {
  render() {
    return (
      <MuiThemeProvider theme={theme}>
        <Provider store={store}>
          <Router>
            <Navbar />
            <div className="container">
              <Switch>
                <RequiredLoginRoute exact path="/" component={home} />
                <AuthRoute exact path="/login" component={login} />
                <AuthRoute exact path="/signup" component={signup} />
                <Route
                  exact
                  path="/category/:categoryId"
                  component={category}
                />
                <Route
                  exact
                  path="/question/:questionId"
                  component={question}
                />
              </Switch>
            </div>
          </Router>
        </Provider>
      </MuiThemeProvider>
    );
  }
}

export default App;
