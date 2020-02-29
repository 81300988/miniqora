
import React, {useState, useRef, useEffect} from 'react';
import PropTypes from 'prop-types';
import withStyles from '@material-ui/core/styles/withStyles';
import  {Component} from 'react';
import CreateAnswerComment from './CreateComment';
import axios from 'axios';
import MyButton from '../../util/MyButton';
import {
    Card,
    Grid,
    CardContent,
    Avatar,
    Typography,
    Divider,
  } from '@material-ui/core';
// Icons
import ChatIcon from '@material-ui/icons/Chat';
import dayjs from 'dayjs';
/**
 * Creates a comment for a post
 */


const styles = {
    card: {
      display: 'flex',
      flex: 1,
      marginBottom: 20,
    },
    content: {
      padding: 25,
      width: '100%',
    },
    padding: {
      padding: '10px 0',
    },
    commentBody: {
      padding: '10px',
      wordWrap: 'break-word',
      fontSize: '14px',
    },
    cmtWrapper: {
      paddingLeft: '30px',
      paddingTop: '20px',
    },
    commentDate: {
      borderBottom: 'none',
      color: '#9199a1',
    },
    commentUser: {
      whiteSpace: 'nowrap',
      padding: '0',
      color: '#0077cc',
    },
    answers: {
      paddingTop: '10px',
    },
    answersHeader: {
      marginTop: '10px',
    },
    answersCount: {
      fontWeight: '400',
      fontSize: '18px',
    },
    answersBlock: {
      padding: '10px 0',
      borderTop: '1px solid rgba(0, 0, 0, 0.12)',
    },
  };
  
  class AnswerForQuestionDetail extends Component {
    state = {
        showAddCommentForAnswer: false,
        comments: [],
        flag: false,
    };
    
    render() {
      const {showAddCommentForAnswer, comments,flag} = this.state;
      const {
        classes,
        answer: {
            answerId,
            answerContent,
            userAnswerId,
            userFullName,
            avatar,
            avatarUrl,
            answerDate,
            totalVote,
            answerComments,
        },
      } = this.props;
  
      const addComment = async (CommentContent, answerId)  => {
        axios
          .post('/api/comment/CommentForAnswer', {
            AnswerId: answerId,
            CommentContent,
          })
          .then(res => {
            this.setState({
                comments: res.data.comments,
                flag: true,
              });
          })
          .catch(err => {
            this.setState({
            });
          });
      };

    const onClickCommentForAnswer = () => {
        this.setState({
          showAddCommentForAnswer: !showAddCommentForAnswer,
        });
        };
  
      return (
        <div className={classes.answersBlock} key={answerId}>
        <Avatar src={`${process.env.REACT_APP_API_URL}${avatarUrl}`} />
        <span>{answerContent}</span><span><MyButton tip="comments" onClick={onClickCommentForAnswer}>
                                                    <ChatIcon color="primary" />
                                                </MyButton></span>
        {this.state.showAddCommentForAnswer&&<CreateAnswerComment answerId = {answerId} addComment={addComment} />}
        <div className={classes.cmtWrapper}>
          {flag?((comments || []).map(cmt => (
            <div key={cmt.questionId}>
              <Divider />
              <div className={classes.commentBody}>
              <Avatar src={`${process.env.REACT_APP_API_URL}${cmt.avatarUrl}`} />
                <span>{cmt.commentContent}</span>
                &nbsp;–&nbsp;
                <a className={classes.commentUser}>
                  {cmt.commenter}
                </a>
                &nbsp;
                <span className={classes.commentDate}>
                {dayjs(cmt.createdDate).fromNow()}
                </span>
              </div>
            </div>
          ))):((answerComments || []).map(cmt => (
            <div key={cmt.questionId}>
              <Divider />
              <div className={classes.commentBody}>
              <Avatar src={`${process.env.REACT_APP_API_URL}${cmt.avatarUrl}`} />
                <span>{cmt.commentContent}</span>
                &nbsp;–&nbsp;
                <a className={classes.commentUser}>
                  {cmt.commenter}
                </a>
                &nbsp;
                <span className={classes.commentDate}>
                {dayjs(cmt.createdDate).fromNow()}
                </span>
              </div>
            </div>
          )))}
        </div>
      </div>
      );
    }
  }
  
  AnswerForQuestionDetail.propTypes = {
    answer: PropTypes.object.isRequired,
    classes: PropTypes.object.isRequired,
    openDialog: PropTypes.bool,
  };
  
  export default withStyles(styles)(AnswerForQuestionDetail);