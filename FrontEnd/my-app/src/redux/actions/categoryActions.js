import {
  SET_CATEGORY_QUESTIONS,
  LOADING_DATA,
  RESET_CATEGORY_PAGE,
  SET_QUESTION,
  LIKE_SCREAM_CAT,
  UNLIKE_SCREAM_CAT,
} from '../types';
import axios from 'axios';
import {get} from 'lodash';

// Get all questions
export const getQuestions = ({categoryId, page = 1}) => dispatch => {
  dispatch({type: LOADING_DATA});
  return axios
    .get('/api/category/GetCategotyPage', {
      params: {id: categoryId, pageIndex: page},
    })
    .then(res => {
      dispatch({
        type: SET_CATEGORY_QUESTIONS,
        payload: get(res, 'data.response'),
      });
    })
    .catch(() => {
      dispatch({
        type: SET_CATEGORY_QUESTIONS,
        payload: [],
      });
    });
};
export const likeScream = answerId => dispatch => {
  axios
    .post(`/api/answer/UpdateVoteForAnswer`, {answerId})
    .then(res => {
      dispatch({
        type: LIKE_SCREAM_CAT,
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
        type: UNLIKE_SCREAM_CAT,
        payload: {totalVote: res.data.totalVoteForAnswer, answerId: answerId},
      });
    })
    .catch(err => console.log(err));
};
export const getQuestionDetail = ({questionId, page = 1}) => dispatch => {
  dispatch({type: LOADING_DATA});
  return axios
    .get('/api/question/GetQuestionDetail', {
      params: {id: questionId, pageIndex: page},
    })
    .then(res => {
      dispatch({
        type: SET_QUESTION,
        payload: get(res, 'data.questions'),
      });
    })
    .catch(() => {
      dispatch({
        type: SET_QUESTION,
        payload: {},
      });
    });
};

export const resetCategoryPage = () => dispatch => {
  dispatch({type: RESET_CATEGORY_PAGE});
};
