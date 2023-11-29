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
    <img src="/OIP.jpg" alt={text} />
    {/* <div>{text}</div> */}
  </div>
);

var a = 0;
var b = 0;
const Map = () => {

  const defaultProps = {
    center: {
      lat: 23.9861321,
      lng: 121.5907741
    },
    zoom: 15
  };

  const [markers, setMarkers] = useState([
    { lat: 23.987030325794812, lng: 121.58808635961898, text: 5 },
    { lat: 23.983388883570274, lng: 121.589409906775, text: 5 },
    // 添加更多的標記...
  ]);

  const [mapState, setMapState] = useState(defaultProps);

  const handleMapChange = (newMapState) => {
    // console.log(newMapState);
    // console.log(mapState);
    setMapState({
      center: newMapState.center,
      zoom: newMapState.zoom
    });
  };

  useEffect(() => {
    console.log(a, b);
    console.log("---------------");
    console.log(markers[0].text, markers[1].text);
    // 在地图状态变化时更新标记
    setMarkers([
      { lat: 23.987030325794812, lng: 121.58808635961898, text: a++ },
      { lat: 23.983388883570274, lng: 121.589409906775, text: b++ }
    ]);
  }, [mapState]);

  return (
    // Important! Always set the container height explicitly

    <div style={{ height: '100vh', width: '100%' }}>
      <GoogleMapReact
        bootstrapURLKeys={{ key: "AIzaSyDtQ_pnDCr2GVlOEC1_7LWpKkdrh34FOkI" }}
        defaultCenter={defaultProps.center}
        defaultZoom={defaultProps.zoom}
        onChange={handleMapChange}
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
          />
        ))}
      </GoogleMapReact>
    </div>
  );
}


export default Map