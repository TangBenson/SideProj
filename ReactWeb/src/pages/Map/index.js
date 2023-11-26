import GoogleMapReact from 'google-map-react';
import React, { useEffect, useState } from 'react';

const AnyReactComponent = ({ text }) => (
  <div style={{
    color: 'white',
    background: 'grey',
    padding: '15px 10px',
    display: 'inline-flex',
    textAlign: 'center',
    alignItems: 'center',
    justifyContent: 'center',
    borderRadius: '100%',
    transform: 'translate(-50%, -50%)'
  }}>
    <img src="../../../OIP.jpg" alt={text} />
    {/* <div>{text}</div> */}
  </div>
);

const Map = () => {

  const defaultProps = {
    center: {
      lat: 23.9861321,
      lng: 121.5907741
    },
    zoom: 15
  };

  const markers = [
    { lat: 23.987030325794812, lng: 121.58808635961898, text: 'car1' },
    { lat: 23.983388883570274, lng: 121.589409906775, text: 'car2' },
    // 添加更多的標記...
  ];
  
  const [mapState, setMapState] = useState(defaultProps);

  const handleMapChange = (newMapState) => {
    setMapState(newMapState);
    console.log('dddd');
  };

  return (
    // Important! Always set the container height explicitly

    <div style={{ height: '100vh', width: '100%' }}>
      <GoogleMapReact
        bootstrapURLKeys={{ key: "AIzaSyDtQ_pnDCr2GVlOEC1_7LWpKkdrh34FOkI" }}
        defaultCenter={defaultProps.center}
        defaultZoom={defaultProps.zoom}
        onChange={handleMapChange}
        // onDrag={handleMapChange}
        options={{
          disableDefaultUI: true
        }}
      >
        {markers.map((marker, index) => (
          <AnyReactComponent
            key={index}
            lat={marker.lat}
            lng={marker.lng}
            text={marker.text}
            onClick={() => console.log(marker.text)}
          />
        ))}
        {/* <AnyReactComponent
          lat={23.987030325794812}
          lng={121.58808635961898}
          text="car1"
          onClick={() => console.log("car1")}
        />
        <AnyReactComponent
          lat={23.983388883570274}
          lng={121.589409906775}
          text="car2"
          onClick={() => console.log("car2")}
        /> */}
      </GoogleMapReact>
    </div>
  );
}


export default Map