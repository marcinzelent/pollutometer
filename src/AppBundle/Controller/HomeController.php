<?php
/**
 * Created by PhpStorm.
 * User: andy
 * Date: 11/23/17
 * Time: 10:21 AM
 */

namespace AppBundle\Controller;

use Sensio\Bundle\FrameworkExtraBundle\Configuration\Route;
use Symfony\Component\HttpFoundation\Response;
use Symfony\Bundle\FrameworkBundle\Controller\Controller;


class HomeController extends Controller
{
    /**
     * @Route("/", name="homepage")
     */

    public function numberAction()
    {
        // Get cURL resource
        $curl = curl_init();
        curl_setopt($curl, CURLOPT_URL, "http://pollutometerapi.azurewebsites.net/api/Readings/latest");
        curl_setopt($curl, CURLOPT_HTTPHEADER, array('Content-type: application/json')); // Assuming you're requesting JSON
        curl_setopt($curl, CURLOPT_RETURNTRANSFER, 1);
        // Send the request & save response to $resp
        $resp = curl_exec($curl);
        // Close request to clear up some resources
        curl_close($curl);

        $data = json_decode($resp, true);
        $data['TimeStamp'] = gmdate("l jS \of F Y h:i:s A", $data['TimeStamp']);


        return $this->render('default/index.html.twig',$data);
    }

}