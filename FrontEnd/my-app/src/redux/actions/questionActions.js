import {
  SET_QUESTIONS,
  LOADING_DATA,
  LOADING_UI,
  POST_QUESTION,
  CLEAR_ERRORS,
  SET_ERRORS,
} from '../types';
import axios from 'axios';
import {get} from 'lodash';

// Post a question
export const postQuestion = params => dispatch => {
  dispatch({type: LOADING_UI});
  axios
    .post('/api/question/AddQuestion', params)
    .then(res => {
      dispatch({
        type: POST_QUESTION,
        payload: res.data,
      });
      dispatch(clearErrors());
    })
    .catch(err => {
      console.log('err', err);

      dispatch({
        type: SET_ERRORS,
        payload: err.response,
      });
    });
};

export const clearErrors = () => dispatch => {
  dispatch({type: CLEAR_ERRORS});
};
