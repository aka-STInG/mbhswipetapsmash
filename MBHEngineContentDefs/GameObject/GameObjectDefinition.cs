﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace MBHEngineContentDefs
{
    public class GameObjectDefinition
    {
        /// <summary>
        /// Different categories of game objects.  Used by both the game object system and 
        /// behaviours for things like limiting collision to a certain group of Game Objects.
        /// </summary>
        public enum Classifications
        {
            PLAYER = 0,
            ENEMY,
            ALLY,
            VOLLEY_BALL,
            WALL,

            COUNT,
        }

        /// <summary>
        /// The different types of blending that a gameobject can use when rendering.
        /// </summary>
        public enum BlendMode
        {
            UNDEFINED = 0,
            STANDARD,
            MULTIPLY,
            STANDARD_UI,
            MULTIPLY_UI,
            TEST
        };

        /// <summary>
        /// The different areas of the screen that an object can be anchored to. Useful for UI elements.
        /// </summary>
        public enum ScreenAnchor
        {
            LEFT = 0,
            TOP,
            RIGHT,
            BOTTOM,
            CENTER,

            COUNT,
        }

        /// <summary>
        /// An anchor point is both the X and Y ScreenAnchor components.
        /// </summary>
        public class ScreenAnchorPoint
        {
            public ScreenAnchor X; // LEFT, CENTER, RIGHT.
            public ScreenAnchor Y; // TOP, CENTER, BOTTOM.
        }

        /// <summary>
        /// Determines the order at which IGameObjects should be rendered.
        /// The higher the number the later it will be rendered.
        /// </summary>
        public Int32 mRenderPriority;

        /// <summary>
        /// Whether on not this object should be updated.
        /// </summary>
        public Boolean mDoUpdate;

        /// <summary>
        /// Whether on not this object should be rendered.
        /// </summary>
        public Boolean mDoRender;

        /// <summary>
        /// The position of the object in space.
        /// </summary>
        public Vector2 mPosition;

        /// <summary>
        /// An anchor point on the screen that this object should be positioned relative to. If this is set,
        /// mPosition become and offset from this AnchorPoint.
        /// </summary>
        [ContentSerializer(Optional = true)]
        public ScreenAnchorPoint mScreenRelativeAnchorPoint;

        /// <summary>
        /// The rotation of the object around the Z axis.
        /// </summary>
        public Single mRotation;

        /// <summary>
        /// The scale of the object in both the x and y axis independently.
        /// </summary>
        public Vector2 mScale;

        /// <summary>
        /// True if this object will not move while being managed by the GameObjectManager.
        /// If it is a GameObjectFactory managed object, it is fine to move it in between
        /// GetTemplate and adding it to the GameObjectManager.
        /// </summary>
        [ContentSerializer(Optional = true)]
        public Boolean mIsStatic;

        /// <summary>
        /// The width and height of the collision box.
        /// </summary>
        [ContentSerializer(Optional = true)]
        public Vector2 mCollisionBoxDimensions;

        /// <summary>
        /// Some objects do not want to use the same offset for collision as the motion root.  This is true
        /// for sprites whhich do not go to the edge of the frame.
        /// </summary>
        [ContentSerializer(Optional = true)]
        public Vector2 mCollisionRoot;

        /// <summary>
        /// The offset that the objects origin can be found.
        /// </summary>
        [ContentSerializer(Optional = true)]
        public Vector2 mMotionRoot;

        /// <summary>
        /// The calssifications for this game object.
        /// </summary>
        [ContentSerializer(Optional = true)]
        public List<Classifications> mClassifications;

        /// <summary>
        /// The type of blending to be used when rendering this object.
        /// </summary>
        [ContentSerializer(Optional = true)]
        public BlendMode mBlendMode = BlendMode.STANDARD;

        /// <summary>
        /// A list of all the behaviours this game object has.  These are indentified by
        /// the name of the definition file for each.
        /// </summary>
        public List<String> mBehaviourFileNames;

        /// <summary>
        /// This should be an index to index mapping with mBehaviorFileNames, where instead
        /// of defining the file names, we are defining the class names of the behaviour.
        /// This needs to be the full class and names space.  For example:
        /// MBHEngine.Behaviour.Behaviour
        /// </summary>
        public List<String> mBehaviourClassNames;
    }
}
