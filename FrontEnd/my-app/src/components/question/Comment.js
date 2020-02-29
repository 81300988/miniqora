import React from 'react';
import PropTypes from 'prop-types';
import {generatePath} from 'react-router-dom';
import withStyles from '@material-ui/core/styles/withStyles';

import {CloseIcon} from '../icons/CloseIcon';
import {UserIcon} from '../icons/UserIcon';
import {Avatar} from '@material-ui/core';

const styles = {
  root: {
    flexDirection: 'row',
    padding: '10px',
    fontSize: '12px',
    border: '1px solid #ddd',
    margin: '5px 0',
  },
  delete: {
    cursor: 'pointer',
    display: 'none',
    backgroundColor: 'transparent',
    border: 0,
    outline: 0,
    position: 'absolute',
    right: '7px',
    top: '6px',
  },
  imageContainer: {
    marginRight: '5px',
    width: '30px',
    height: '30px',
    borderRadius: '50%',
    overflow: 'hidden',
  },
  commentSection: {
    position: 'relative',
    wordWrap: 'break-word',
    overflow: 'hidden',
  },
  avatar: {
    display: 'flex',
  },
  content: {
    display: 'flex',
  },
};

/**
 * Renders comments UI
 */
const Comment = ({comment, classes}) => {
  const {avatarUrl, commentContent, commenter} = comment;
  const handleDeleteComment = async () => {};

  return (
    <div className={classes.root}>
      <div className={classes.avatar}>
        <a>
          <div className={classes.imageContainer}>
            {avatarUrl ? (
              <Avatar src={`${process.env.REACT_APP_API_URL}${avatarUrl}`} />
            ) : (
              <UserIcon width="30" />
            )}
          </div>
        </a>

        <div className={classes.commentSection}>
          <div className={classes.delete} onClick={() => handleDeleteComment()}>
            <CloseIcon width="10" />
          </div>
          {commenter}
        </div>
      </div>
      <div className={classes.content}>{commentContent}</div>
    </div>
  );
};

Comment.propTypes = {
  comment: PropTypes.object.isRequired,
};

export default withStyles(styles)(Comment);
