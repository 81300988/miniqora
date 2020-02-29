import {
  SET_QUESTIONS,
  LOADING_DATA,
  LIKE_SCREAM,
  UNLIKE_SCREAM,
  DELETE_SCREAM,
  SET_ERRORS,
  POST_SCREAM,
  CLEAR_ERRORS,
  LOADING_UI,
  STOP_LOADING_UI,
  SUBMIT_COMMENT,
  SET_CATEGORIES,
  RESET_HOME,
  USE_CACHE,
} from '../types';
import axios from 'axios';
import {get} from 'lodash';
import ProfileSkeleton from '../../util/ProfileSkeleton';

// Get all questions
export const getQuestions = (pageIndex = 1) => dispatch => {
  dispatch({type: LOADING_DATA});
  let url =`/api/question/GetQuestions?pageIndex=${pageIndex}`;

  if(USE_CACHE!=='false'){
    url =`/api/redis/RedisGetQuestions?pageIndex=${pageIndex}`;
  }
  axios
    .get(url)
    .then(res => {
      dispatch({
        type: SET_QUESTIONS,
        payload: get(res, 'data.response', {}),
      });
    })
    .catch(err => {
      dispatch({
        type: SET_QUESTIONS,
        payload: [],
      });
    });
};
export const getCategories = () => dispatch => {
  dispatch({type: LOADING_DATA});
  axios
    .get('/api/category/GetCategories')
    .then(res => {
      dispatch({
        type: SET_CATEGORIES,
        payload: res.data,
      });
    })
    .catch(() => {
      dispatch({
        type: SET_CATEGORIES,
        payload: [],
      });
    });
};

// Post a scream
export const postScream = newScream => dispatch => {
  dispatch({type: LOADING_UI});
  axios
    .post('/scream', newScream)
    .then(res => {
      dispatch({
        type: POST_SCREAM,
        payload: res.data,
      });
      dispatch(clearErrors());
    })
    .catch(err => {
      dispatch({
        type: SET_ERRORS,
        payload: err.response.data,
      });
    });
};
// Like a scream
export const likeScream = answerId => dispatch => {
  axios
    .post(`/api/answer/UpdateVoteForAnswer`, {answerId})
    .then(res => {
      dispatch({
        type: LIKE_SCREAM,
        payload: {totalVote: res.data.totalVoteForAnswer, answerId: answerId},
      });
    })
    .catch(err => console.log(err));
};
// Unlike a scream
export const unlikeScream = answerId => dispatch => {
  axios
    .post(`/api/answer/UpdateVoteForAnswer`, {answerId})
    .then(res => {
      dispatch({
        type: UNLIKE_SCREAM,
        payload: {totalVote: res.data.totalVoteForAnswer, answerId: answerId},
      });
    })
    .catch(err => console.log(err));
};
// Submit a comment
export const submitComment = (screamId, commentData) => dispatch => {
  axios
    .post(`/scream/${screamId}/comment`, commentData)
    .then(res => {
      dispatch({
        type: SUBMIT_COMMENT,
        payload: res.data,
      });
      dispatch(clearErrors());
    })
    .catch(err => {
      dispatch({
        type: SET_ERRORS,
        payload: err.response.data,
      });
    });
};
export const deleteScream = screamId => dispatch => {
  axios
    .delete(`/scream/${screamId}`)
    .then(() => {
      dispatch({type: DELETE_SCREAM, payload: screamId});
    })
    .catch(err => console.log(err));
};

export const resetHome = () => dispatch => {
  dispatch({type: RESET_HOME});
};

export const clearErrors = () => dispatch => {
  dispatch({type: CLEAR_ERRORS});
};
