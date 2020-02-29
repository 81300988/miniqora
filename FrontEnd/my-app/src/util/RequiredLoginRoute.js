import React from 'react';
import {Route, Redirect} from 'react-router-dom';
import {connect} from 'react-redux';
import PropTypes from 'prop-types';

const RequiredLoginRoute = ({component: Component, authenticated, ...rest}) => (
  <Route
    {...rest}
    render={props =>
      !authenticated ? <Redirect to="/login" /> : <Component {...props} />
    }
  />
);

const mapStateToProps = state => ({
  authenticated: state.user.authenticated,
});

RequiredLoginRoute.propTypes = {
  user: PropTypes.object,
};

export default connect(mapStateToProps)(RequiredLoginRoute);
